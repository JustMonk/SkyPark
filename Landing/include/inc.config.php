<?php 
$mysqli = @new mysqli(
    'localhost',  // Õîñò, ê êîòîðîìó ìû ïîäêëþ÷àåìñÿ
    'root',       // Èìÿ ïîëüçîâàòåëÿ
    '',   // Èñïîëüçóåìûé ïàðîëü
    'skypark');     // Áàçà äàííûõ äëÿ çàïðîñîâ ïî óìîë÷àíèþ

//Проверка на ошибки
if ($mysqli->connect_error) {
	echo "Íå óäàëîñü ïîäêëþ÷èòüñÿ ê MySQL: (" . $mysqli->connect_errno . ") " . $mysqli->connect_error;
}
?>