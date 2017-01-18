<?php
include 'inc.config.php';

$func_switch = $_POST['function'];
switch ($func_switch) {
	case 'long':
	long($_POST['days'],$_POST['promo'],$_POST['typets'],$_POST['service']);
	break;
	case 'short':
	short($_POST['hours'],$_POST['promo'],$_POST['typets'],$_POST['service']);
	break;
	case 'dataupload':
	dataupload($_POST['array']);
	break;
	default:
		# code...
	break;
}

function setting($sex, $tour){
	if($sex == 'all' or $sex == 'male' or $sex == 'female'){
		$_SESSION['sex']=$sex;
		if($tour <= 0){
			echo ("<div class=\"alert alert-error\">
				<button class=\"close\" data-dismiss=\"alert\">X</button>
				<h4>Неверный тур</h4>
				Что-то вы затеваете...
			</div>");
		} else {
			$_SESSION['tour']=$tour;
			echo ("<div class=\"alert alert-success\">
				<button class=\"close\" data-dismiss=\"alert\">X</button>
				<h4>Настройки успешно применены</h4>
				Ой, у вас получилось...!
			</div>");
		}
	} else {
		echo ("<div class=\"alert alert-error\">
			<button class=\"close\" data-dismiss=\"alert\">X</button>
			<h4>Неверный пол</h4>
			Извините, но вы не с нашей планеты!
		</div>");
	}
}

function long($days,$code,$typets,$service){
	global $mysqli;
	$message = array();
	$mysqli->query('SET NAMES "utf8"');
	$sql = "SELECT name, price_time FROM tarif WHERE active='1' and type!='Промокод'";
	if ($result = $mysqli->query($sql)) {
		$data = $result->fetch_all();
		$tarif = array();
		foreach($data as $key => $value) {
 		 $tarif[$value['0']] = $value['1']; // тут возвращаете как вам хочется
 		}
 	}
 	$sql = "SELECT name, price_time FROM tarif WHERE active='1' and type='Промокод' and name='$code'";
 	if ($result = $mysqli->query($sql)) {
 		if ($result->num_rows == 0 ){
 			$message['promo'] =  "Промокод: недействителен!";
 			$promocode = false;
 		} else {
 			$data = $result->fetch_assoc();
 			$message['promo'] = "Промокод: принят! (".$data['price_time']."%)";
 			$promocode = true;
 		}
 	}
 	$mes = (int)($days / 30);
 	$days = $days % 30;
 	$summa=(($mes*$tarif['Месяц'])+($days*$tarif['Посуточная']))*$tarif[$typets]/100;
 	//УСЛУГА
 	$services = json_decode($service);
 	if (empty($services)){
 			$message['services1'] = "empty";
 	} else {
 		foreach ($services as $key => $value) {
 			$message['services1'] = "yes";
 			$summa += $summa*$tarif[$value]/100;
 		}
 	}
 	$message['services'] = json_decode($service);
 	 //ПРОМОКОД
 	if ($promocode == true) {
 		$summa=$summa-$summa*$data['price_time']/100;
 	}
 	$message['summa'] = "Итоговая сумма: ".$summa." руб.";
 	echo json_encode($message);
 }

 function short($hours,$code,$typets,$service){
	global $mysqli;
	$message = array();
	$mysqli->query('SET NAMES "utf8"');
	$sql = "SELECT name, price_temp FROM tarif WHERE active='1' and type!='Промокод'";
	if ($result = $mysqli->query($sql)) {
		$data = $result->fetch_all();
		$tarif = array();
		foreach($data as $key => $value) {
 		 $tarif[$value['0']] = $value['1']; // тут возвращаете как вам хочется
 		}
 	}
 	$sql = "SELECT name, price_temp FROM tarif WHERE active='1' and type='Промокод' and name='$code'";
 	if ($result = $mysqli->query($sql)) {
 		if ($result->num_rows == 0 ){
 			$message['promo'] =  "Промокод: недействителен!";
 			$promocode = false;
 		} else {
 			$data = $result->fetch_assoc();
 			$message['promo'] = "Промокод: принят! (".$data['price_temp']."%)";
 			$promocode = true;
 		}
 	}
 	$summa=($hours*$tarif['Почасовая'])*$tarif[$typets]/100;
 	//УСЛУГА
 	$services = json_decode($service);
 	if (empty($services)){
 			$message['services1'] = "empty";
 	} else {
 		foreach ($services as $key => $value) {
 			$message['services1'] = "yes";
 			$summa += $summa*$tarif[$value]/100;
 		}
 	}
 	$message['services'] = json_decode($service);
 	 //ПРОМОКОД
 	if ($promocode == true) {
 		$summa=$summa-$summa*$data['price_temp']/100;
 	}
 	$message['summa'] = "Итоговая сумма: ".$summa." руб.";
 	echo json_encode($message);
 }

 function dataupload($array_source){
 	global $mysqli;
 	$mysqli->query('SET NAMES "utf8"');
 	$tourtemp = "tour_" + $_SESSION['tour'];
 	$array_source = json_decode($array_source);
 	foreach ($array_source as $key => $value) {
 		$sql = "UPDATE tour_" . $_SESSION['tour'] . " FROM users ORDER BY id ASC";
 		if ($result = $mysqli->query($sql)) {
 			$json = array();
 			$i = 0;
 			while($data = mysqli_fetch_array($result)){
 				$i++; 
 				$resultdatatab .= "<tr>
 				<th class=\"id_datatab_valid" . $i . "\" scope=\"row\">" . $data['id'] . "</th>
 				<td>" . $data['sex'] . "</td>
 				<td>" . $data['fio'] . "</td>
 				<td><input type=\"text\" class=\"input_datatab\" value=\"" . $data[3] . "\"></td>
 			</tr>";
 		}
 	}

 	$array_result .= $key . ">" . $value . " ";
 }
 echo $array_result;
}

//echo json_encode("END");
?>
