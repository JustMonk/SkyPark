<?
include "include/inc.config.php";
function settings($value){
	global $mysqli;
	$sql = "SELECT value FROM settings WHERE name = '" . $value . "'";
	if ($result = $mysqli->query($sql)) {
		$data = mysqli_fetch_array($result);
		return $data['value'];
	}
};

function promo(){
	global $mysqli;
	$sql = "SELECT name, price_temp FROM tarif WHERE type = 'Промокод' and active = '1'";
	if ($result = $mysqli->query($sql)) {
		$data = mysqli_fetch_array($result);
		return $data;
	}
};

function usluga(){
	global $mysqli;
	$sql = "SELECT name, price_temp FROM tarif WHERE type = 'Услуга' and active = '1'";
	if ($result = $mysqli->query($sql)) {
		while ($data = mysqli_fetch_array($result)) {
			echo "<label><input name=\"usluga\" value=\"".$data['name']."\" type=\"checkbox\">".$data['name']."</label> <br>";
		}
	}
};

function type(){
	global $mysqli;
	$sql = "SELECT name, price_temp FROM tarif WHERE type = 'Стандарт' and active = '1'";
	if ($result = $mysqli->query($sql)) {
		while ($data = mysqli_fetch_array($result)) {
			echo "<option value=\"".$data['name']."\">".$data['name']."</option>";
		}
	}
};
?>
<!DOCTYPE html>
<html lang="ru">

<head>
	<meta charset="utf-8" />
	<title><? echo(settings('sitename')); ?></title>
	<meta name="description" content="" />
	<meta http-equiv="X-UA-Compatible" content="IE=edge" />
	<meta name="viewport" content="width=device-width, initial-scale=1.0" />
	<link rel="shortcut icon" href="favicon.png" />
	<!--17: Full bootstrap library -->
	<link rel="stylesheet" href="libs/bootstrap/bootstrap.css" />
	<link rel="stylesheet" href="libs/font-awesome-4.2.0/css/font-awesome.min.css" />
	<!--<link rel="stylesheet" href="libs/fancybox/jquery.fancybox.css" />-->
	<link rel="stylesheet" href="libs/owl-carousel/owl.carousel.css" />
	<!--<link rel="stylesheet" href="libs/countdown/jquery.countdown.css" />-->
	<link rel="stylesheet" href="css/fonts.css" />
	<link rel="stylesheet" href="css/main.css" />
	<link rel="stylesheet" href="css/media.css" />
	<!-- include css files (disc) -->
	<!--<link rel="stylesheet" href="css/include/menu.css" />
	<link rel="stylesheet" href="css/include/slider.css" />-->
</head>

<body>
	<<script src="libs/jquery/jquery-1.11.1.min.js"></script>
	<!--<script src="libs/jquery-mousewheel/jquery.mousewheel.min.js"></script>-->
	<!--<script src="libs/fancybox/jquery.fancybox.pack.js"></script>-->
	<!--<script src="libs/waypoints/waypoints-1.6.2.min.js"></script>-->
	<script src="libs/scrollto/jquery.scrollTo.min.js"></script>
	<script src="libs/owl-carousel/owl.carousel.min.js"></script>
	<!--<script src="libs/countdown/jquery.plugin.js"></script>-->
	<!--<script src="libs/countdown/jquery.countdown.min.js"></script>-->
	<!--<script src="libs/countdown/jquery.countdown-ru.js"></script>-->
	<!--<script src="libs/landing-menu_top/menu_topigation.js"></script>-->
	<!-- 45: Bootstrap JS -->
	<script src="js/bootstrap.js"></script>
	<!-- Yandex.Metrika counter --><!-- /Yandex.Metrika counter -->
	<!-- Google Analytics counter --><!-- /Google Analytics counter -->


	<div id="scroll-animate">
		<div id="scroll-animate-main">
			<div class="wrapper-parallax">

				<!-- ===================fixed navbar====================== -->
				<nav class="navbar navbar-default navbar-fixed-top wow fadeInDown" id="fixnavigation">
					<div class="container-fluid">
						<!-- Brand and toggle get grouped for better mobile display -->
						<div class="navbar-header">
							<button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1" aria-expanded="false">
								<span class="sr-only">Toggle navigation</span>
								<span class="icon-bar"></span>
								<span class="icon-bar"></span>
								<span class="icon-bar"></span>
							</button>
							<a class="navbar-brand header_brand" href="https://github.com/JustMonk/SkyPark" target="_blank"><i class="fa fa-github" aria-hidden="true"></i> <? echo(settings('sitename')); ?></a>
						</div>

						<!-- Collect the nav links, forms, and other content for toggling -->
						<div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
							<ul class="nav navbar-nav">
								<!-- активная <li class="active"><a href="#pluses">Преимущества<span class="sr-only">(current)</span></a></li> -->
								<li><a href="#scroll-animate">Парковка</a></li>
								<li><a href="#pluses">Преимущества<span class="sr-only">(current)</span></a></li>
								<li><a href="#promo">Акция</a></li>
								<li><a href="#calculator">Калькулятор</a></li>
								<li><a href="#inform">Информация</a></li>
								<!--<li><a href="#">Партнеры</a></li>-->

							</ul>

							<ul class="nav navbar-nav navbar-right">
								<li style="background-color: #5d6f8f;"><a style="color: white;"><? echo(settings('address')); ?></a></li>
								<li style="background-color: #2B405D;"><a style="color: white;">Телефон: <? echo(settings('phone')); ?></a></li>
								<!--<p class="navbar-text navbar-right">Signed in as</p>-->
							</ul>
						</div><!-- /.navbar-collapse -->
					</div><!-- /.container-fluid -->
				</nav>

				<header>
					<div class="block1">

						<div class="block1_content" id="header_block">
							Многоуровневый стояночный комплекс <b><? echo(settings('sitename')); ?></b>
							<br>
							<a class="calculator_button" href="#calculator">Расчитать стоимость</a>	
							<div class="status_avto">
								Узнайте статус вашего автомобиля
								<br>
								<!--<input type="text" class="code_input"></input>-->
								<div class="row">
									<div class="col-lg-12">
										<div class="input-group code_input">
											<input type="text" class="form-control" placeholder="Введите код с чека">
											<span class="input-group-btn">
												<button class="btn btn-primary" type="button">Go!</button>
											</span>
										</div><!-- /input-group -->
									</div><!-- /.col-lg-6 -->
								</div><!-- /.row -->
								<font class="status_description">Код с чека в формате XXXX-XXXX-XXXX-XXXX</font>

							</div>

						</div>

					</div>
				</header>


				<section class="content" style="height: auto;min-height: auto;">

					<div class="block2" id="pluses">
						<h1>Наши преимущества</h1>

						<div class="cards wow fadeInRight">
							<div class="card">
								<div class="card-bg"></div>
								<div class="card-content"><span class="label"><i class="fa fa-clock-o fa-5x" aria-hidden="true" style="margin-bottom:15px;color: #434654;"></i><br>24/7</span><span class="title"><hr></span>
									<p class="description">Стоянка работает круглосуточно, 7 дней в неделю. Вам всегда рады!</p>
								</div>
							</div>
							<div class="card">
								<div class="card-bg"></div>
								<div class="card-content"><span class="label"><i class="fa fa-credit-card fa-5x" aria-hidden="true" style="margin-bottom:15px;color: #434654;"></i><br>Платите как вам удобно</span><span class="title"><hr></span>
									<p class="description">Наши терминалы поддерживают все способы оплаты, включая онлайн банкинг. Также доступна рассрочка платежа.</p>
								</div>
							</div>
							<div class="card">
								<div class="card-bg"></div>
								<div class="card-content"><span class="label"><i class="fa fa-shield fa-5x" aria-hidden="true" style="margin-bottom:15px;color: #434654;"></i><br>Надежная защита</span><span class="title"><hr></span>
									<p class="description">Собственная система защиты и частное охранное предпреятие, позволят вам не беспокоится о сохранности вашего автомобиля.<br</p>
								</div>
							</div>
							<div class="card">
								<div class="card-bg"></div>
								<div class="card-content"><span class="label"><i class="fa fa-money fa-5x" aria-hidden="true" style="margin-bottom:15px;color: #434654;"></i><br>Выгодные тарифы и бонусы</span><span class="title"><hr></span>
									<p class="description">Мы реализуем выгодные акции и бонусы постоянным клиентам, включая систему накопительных и скидочных карт.</p>
								</div>
							</div>

						</div>

					</div>				

				</section>

				<section class="content" id="promo" style="height: auto;min-height: auto;">
					<div class="block3">
						<h1>Специальное предложение</h1>
						<div class="row" style="margin-top: 100px;">
							<div class="col-md-6 wow fadeInLeftBig" style="background: url(bgs/BG-PNG.png) no-repeat -20px -20px;background-size: 320px;background-position: -210px -97px;">
								Введите промокод при оплате и получите скидку в <b><? $promo=promo(); echo $promo['price_temp'];?>%</b>
								<!--<h5 class="offer-card__heading txt-brand-primary">15<span class="txt-brand-primary">%<p class="txt-brand-primary">OFF</p></span></h5>-->

							</div>

							<div class="col-md-6 wow fadeInRightBig" style="background: url(bgs/BG-PNG.png) no-repeat -20px -20px;background-size: 320px;background-position: 550px -110px;">
								<div class="promocode"><? $promo=promo(); echo $promo['name'];?></div>
								<div class="offer-tag">Промокод</div>
							</div>

						</div>
					</div>
				</section>

				<section class="content" id="calculator" style="height: auto;min-height: auto; background-image: url(bgs/calculator_bg_compressed.jpg);background-attachment: scroll;background-repeat: no-repeat;background-size: cover; background-position: 0 40%;">
					<div class="block4">
						<h1>Калькулятор стоимости</h1>
						<br>

						<div class="calculator_wrapper">

							<div class="container" style="width: inherit;">
								<form id="calc" method="POST" action="">
									<ol class="list-theme">
										<li>
											<h2 class="h-theme">Выберите тариф</h2>
											<div class="mk-selectmenu-container" onchange="lala()">
												<div class="form-group">
													<select class="form-control" id="sel1" title="Choose one of the following..." onchange="lala()">
														<option value="long">Долгосрочный</option>
														<option value="short">Краткосрочный</option>
													</select>
												</div>
											</div>
											<h2 class="h-theme">Выберите тип ТС</h2>
											<div class="mk-selectmenu-container">
												<div class="form-group">
													<select class="form-control" id="sel2" title="Choose one of the following...">
														<? echo type(); ?>
													</select>
												</div>
											</div>
										</li>
										<li style="" id="con2">
											<h2 class="h-theme">Введите планируемое время пребывания</h2>
											<div class="mk-selectmenu-container">
												<div class="form-group" id="in2">
													<input id="days" type="text" class="form-control" id="exampleInputEmail1" placeholder="Дней" style="width: 100%; display: inline; display: none;">
													<input id="hours" type="text" class="form-control" id="exampleInputEmail1" placeholder="Часов" style="width: 100%;display: inline; display: none;">
												</div>
											</div>
										</li>
										<li style="" id="con3">
											<h2 class="h-theme">Дополнительные опции</h2>
											<div class="mk-selectmenu-container">
												<div class="form-group" style="" id="in4">
													<div class="checkbox" style="" id="in3">
														<? echo usluga(); ?>
													</div>
													<div class="form-group" style="" id="in4">
														<input type="text" class="form-control" id="exampleInputEmail1" placeholder="Промокод">
													</div>
												</div>
											</div>
										</li>
									</ol>

									<button id="calc_submit" class="btn btn-default calculate_button">Расчитать стоимость</button>

									<div class="alert alert-info" role="alert">
										<div id="promo_code">Наличие промокода: </div>
										<div id="summa">Итоговая сумма: </div>
									</div>

								</form>
							</div>

						</div>
						

					</div>
				</section>


				<section class="content" style="height: auto;min-height: auto;" id="inform">
					<div class="block5">
						<!--<h1>Block5</h1>-->
						<div class="row">
							<div class="col-md-4 first_end_gridblock">
								<h1><i class="fa fa-phone fa-lg" aria-hidden="true"></i></h1>
								Остались вопросы?<br><b>+7 (771) 772-72-73</b>
							</div>

							<div class="col-md-4 middle_gridblock">
								<h1><i class="fa fa-envelope-o fa-lg" aria-hidden="true"></i></h1>
								Предложения и замечания<br><b>skyboxpark@skybox.com</b>
							</div>

							<div class="col-md-4 first_end_gridblock">
								<h1><i class="fa fa-github fa-lg" aria-hidden="true"></i></h1>
								Репозиторий проекта<br><b>github.com/JustMonk/SkyPark</b>
							</div>
						</div>


					</div>
				</section>

				<section class="content" style="height: auto;min-height: auto; background-image: url(bgs/footer_bg_compressed.jpg);background-attachment: scroll;background-repeat: no-repeat;background-size: cover; background-position: 0 0;">
					<div class="block6">
						<h1>SkyPark, ваш выбор!</h1>
						
						<div class="row">
							<div class="col-md-4 footer_grid">
								<p class="footer_grid_title">О нас</p>
								
								<ul class="footer_grid_list">
									<li><a href="https://github.com/JustMonk/SkyPark" target="_blank"><i class="fa fa-github" aria-hidden="true"></i> Проект</a></li>
									<li><a href="https://github.com/JustMonk" target="_blank"><i class="fa fa-github" aria-hidden="true"></i> Другие проекты</a></li>
									<li>SkyBox Devgroup © 2016</li>
								</ul>

							</div>

							<div class="col-md-4 footer_grid">
								<p class="footer_grid_title">Информация</p>
								<ul class="footer_grid_list">
									<li>Версия UI 1.1b</li>
									<li>Backend версия 1.0 calc</li>
									<li>Polish bulid 0.2b</li>
									<li>Image build: compressed</li>
								</ul>
							</div>

							<div class="col-md-4 footer_grid">
								<p class="footer_grid_title">Контакты</p>
								<ul class="footer_grid_list">
									<li><i class="fa fa-vk" aria-hidden="true"></i><a href="https://vk.com/monk_gg" target="_blank"> Анатолий Осипов</a></li>
									<li>Frontend/C# developer</li>
									<li><i class="fa fa-vk" aria-hidden="true"></i><a href="https://vk.com/ivan.rychkov" target="_blank"> Рычков Иван</a></li>
									<li>Backend/SQL developer</a></li>
								</ul>
							</div>

						</div>
					</div>
				</section>

				<!-- link block -->
				<section class="content">
					<div class="block7">
						<!--dummy-->
					</div>
				</section>





			</div>
		</div>
	</div><!-- wrapper end -->

	<!-- including libs -->
	<script src="js/monk_libs/parallax.js"></script>
	<script src="js/monk_libs/cards.js"></script>
	<script src="js/monk_libs/wow.js"></script>
	<script src="js/monk_libs/scroll_init.js"></script>
	<script src="js/ajax.js"></script>
	<link rel="stylesheet" href="css/include/cards.css" />
	<link rel="stylesheet" href="css/include/animate.css" />

	<script>
		new WOW().init();
	</script>


	<script type="text/javascript">
		window.onload = Abc;  
		function Abc(){          
			var val = document.getElementById('sel1');
			val.selectedIndex=-1;

		}  
		function lala(){          
			console.log('lala is correcly work !');
			var input2 = document.getElementById('con2');
			input2.style.display = "block";

			var selected_type = document.getElementById('sel1');
			if (selected_type.selectedIndex==0)
			{
				document.getElementById('days').style.display = "block";
				document.getElementById('hours').style.display = "none";

			}
			else
			{
				document.getElementById('hours').style.display = "block";
				document.getElementById('days').style.display = "none";
			}

		}       

	</script>


</body>
</html>

