$(document).ready(function() {

//INPUT ПРОВЕРКА
var s = document.getElementById("in3");
d = s.querySelectorAll('input[type="checkbox"]:not([value]), input[type="checkbox"][value=""]');
  for (var i = 0; i < d.length; i++) // чтобы не было написано NaN, убираем в disabled пункты, где не прописаны значения
  	d[i].disabled = true;
s.onchange = function() { // начало работы функции сложения
	var n = s.querySelectorAll('[type="checkbox"]'),
	itog = 0;
	var uslugi = "usluga=";
	for(var j=0; j<n.length; j++)
		n[j].checked ? uslugi += n[j].value : uslugi;
	console.log(uslugi);

}


//-------ОТПРАВКА НАСТРОЕК----------
$('#calc').submit(function (e){
	console.log('Отправка данных с формы калькулятор:'); //ТЕСТ* показ в логах
	e.preventDefault(); //отмена стандартных действий
	//var val = $("#id_setting_sex").find("option:selected").val();
	var service = new Array();
	$("input[name=\"usluga\"]:checked").each(function() {service.push($(this).val());});
	console.log(service);
	//
	var n = document.getElementById("sel1").options.selectedIndex;
	if (n >= 0) {
		var type = document.getElementById("sel1").options[n].value;
		var n = document.getElementById("sel2").options.selectedIndex;
		if (n >= 0) {
			var typets = document.getElementById("sel2").options[n].value;
			var source = "function="+type+"&days="+document.getElementById("days").value+"&hours="+document.getElementById("hours").
			value+"&promo="+document.getElementById("exampleInputEmail1").value+"&typets="+typets+"&service="+JSON.stringify(service);
		console.log(source); //ТЕСТ* показ в логах
		$.ajax({
			type: "POST",
			url: "../include/ajax.calc.php",
			data: source,
			dataType: 'json',
			cache: false,
			success: function(result){
				console.log(result);
				console.log(result.summa);
				console.log(result.promo);
				$("#summa").html(result.summa);
				$("#promo_code").html(result.promo);
			}
		});
	}
} else {
			console.log('Не выбран тип!'); //ТЕСТ* показ в логах
		}
	});
//_________________


//-------ПОЛУЧЕНИЕ ДАННЫХ----------
$('#id_button_datatab_reload').click(function (){
	console.log('RELOAD'); //ТЕСТ* показ в логах
	var source = "function=datatable";
	console.log(source); //ТЕСТ* показ в логах
	$.ajax({
		type: "POST",
		url: "ajax_function.php",
		data: source,
		cache: false,
		success: function(txt){
			//console.log(txt);
			if (!txt) {
				$("#id_datatab_infopanel").html('<div class=\"alert alert-error\"> <button class=\"close\" data-dismiss=\"alert\">X</button> <h4>Неверные настройки</h4> Что-то вы затеваете... </div>');
			} else {
				$("#id_datatab_result").html(txt);
				document.getElementById('myPager').innerHTML = '';
				$('#id_datatab_result').pageMe({pagerSelector:'#myPager',showPrevNext:true,hidePageNumbers:false,perPage:10});
			}
		}
	});
});
//_________________

//-------ОТПРАВКА РЕЗУЛЬТАТОВ----------
$('#id_button_datatab_upload').click(function (){
	console.log('UPLOA2D22'); //ТЕСТ* показ в логах
	var editarray = new Array();
	$( ".ChangeValue" ).each(function( index ) {
		var elements = $(this).parents();
		var id = $(this).parent().siblings(".id_datatab_valid").text()
		var value = $(this).val();
		editarray[id] = value;
		//editarray.push(id + ":" + value);
		console.log(id + ":" + value);
		console.log(editarray);
	});
	$obj = $.extend({}, editarray);
	console.log($obj);
	var source = "function=dataupload&array=" + JSON.stringify($obj);
	console.log(source); //ТЕСТ* показ в логах
	$.ajax({
		type: "POST",
		url: "ajax_function.php",
		data: source,
		cache: false,
		success: function(txt){
			//console.log(txt);
			if (!txt) {
				$("#id_datatab_infopanel").html('<div class=\"alert alert-error\"> <button class=\"close\" data-dismiss=\"alert\">X</button> <h4>Неверные настройки</h4> Что-то вы затеваете... </div>');
			} else {
				$("#id_datatab_result").html(txt);
				document.getElementById('myPager').innerHTML = '';
				$('#id_datatab_result').pageMe({pagerSelector:'#myPager',showPrevNext:true,hidePageNumbers:false,perPage:10});
			}
		}
	});
});
//_________________

//-------ОТПРАВКА РЕЗУЛЬТАТОВ----------

$("#id_datatab_result").on("change", "input", function() {
  // Does some stuff and logs the event to the console
  console.log('Смена5'); //ТЕСТ* показ в логах
  $(this).addClass('ChangeValue');
  console.log($(this));
});


//_________________
});


$.fn.pageMe = function(opts){
	var $this = this,
	defaults = {
		perPage: 7,
		showPrevNext: false,
		hidePageNumbers: false
	},
	settings = $.extend(defaults, opts);

	var listElement = $this;
	var perPage = settings.perPage; 
	var children = listElement.children();
	var pager = $('.pager');

	if (typeof settings.childSelector!="undefined") {
		children = listElement.find(settings.childSelector);
	}

	if (typeof settings.pagerSelector!="undefined") {
		pager = $(settings.pagerSelector);
	}

	var numItems = children.size();
	var numPages = Math.ceil(numItems/perPage);

	pager.data("curr",0);

	if (settings.showPrevNext){
		$('<li><a href="#" class="prev_link">«</a></li>').appendTo(pager);
	}

	var curr = 0;
	while(numPages > curr && (settings.hidePageNumbers==false)){
		$('<li><a href="#" class="page_link">'+(curr+1)+'</a></li>').appendTo(pager);
		curr++;
	}

	if (settings.showPrevNext){
		$('<li><a href="#" class="next_link">»</a></li>').appendTo(pager);
	}

	pager.find('.page_link:first').addClass('active');
	pager.find('.prev_link').addClass('disabled');
	if (numPages<=1) {
		pager.find('.next_link').addClass('disabled');
	}
	pager.children().eq(1).addClass("active");

	children.hide();
	children.slice(0, perPage).show();

	pager.find('li .page_link').click(function(){
		var clickedPage = $(this).html().valueOf()-1;
		goTo(clickedPage,perPage);
		return false;
	});
	pager.find('li .prev_link').click(function(){
		previous();
		return false;
	});
	pager.find('li .next_link').click(function(){
		next();
		return false;
	});

	function previous(){
		var goToPage = parseInt(pager.data("curr")) - 1;
		goTo(goToPage);
	}

	function next(){
		goToPage = parseInt(pager.data("curr")) + 1;
		goTo(goToPage);
	}

	function goTo(page){
		var startAt = page * perPage,
		endOn = startAt + perPage;

		children.css('display','none').slice(startAt, endOn).show();

		if (page>=1) {
			pager.find('.prev_link').removeClass("disabled");
		}
		else {
			pager.find('.prev_link').addClass('disabled');
		}

		if (page<(numPages-1)) {
			pager.find('.next_link').removeClass("disabled");
		}
		else {
			pager.find('.next_link').addClass('disabled');
		}

		pager.data("curr",page);
		pager.children().removeClass("active");
		pager.children().eq(page+1).addClass("active");

	}
};
