//для справки: на данный момент все довольно таки адаптивно, высота документа устанавливается при загрузке страницы, но для полной адаптивности можно поставить ее в событие window.size.changed(). а пока чтобы посмотреть мобил.версию нужно ужать экран, а потом (с ужатым) обновить страницу

function scrollFooter(scrollY, heightFooter)
		{
			console.log(scrollY);
			console.log(heightFooter);

			if(scrollY >= heightFooter)
			{
				$('footer').css({
					'bottom' : '0px'
				});
			}
			else
			{
				$('footer').css({
					'bottom' : '-' + heightFooter + 'px'
				});
			}
		}
		$(window).load(function(){
			var windowHeight        = $(window).height(),
			footerHeight        = $('footer').height(),
			heightDocument      = (windowHeight) + ($('.content').height() + $('.block3').height() + $('.block4').height() + $('.block5').height() + $('.block6').height() + $('.block7').height() ) + ($('footer').height()) - 20;

    // Definindo o tamanho do elemento pra animar
    $('#scroll-animate, #scroll-animate-main').css({
    	'height' :  heightDocument + 'px'
    });

    // Definindo o tamanho dos elementos header e conteúdo
    $('header').css({
    	//'height' : windowHeight + 'px',
    	//'line-height' : windowHeight + 'px'
    });

    $('.wrapper-parallax').css({
    	'margin-top' : windowHeight - 100 + 'px' // windowHeight - 100 + 'px'
    });

    scrollFooter(window.scrollY, footerHeight);

    // ao dar rolagem
    window.onscroll = function(){
    	var scroll = window.scrollY;

    	$('#scroll-animate-main').css({
    		'top' : '-' + scroll + 'px'
    	});

    	$('header').css({
    		'background-position-y' : 50 - (scroll * 100 / heightDocument) + '%'
    	});

    	scrollFooter(scroll, footerHeight);
        //console.log('current windowHeight = ' + heightDocument); логи
    }
  });