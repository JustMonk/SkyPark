jQuery(function( $ ){
	/**
	 * Demo binding and preparation, no need to read this part
	 */
		//borrowed from jQuery easing plugin
		//http://gsgd.co.uk/sandbox/jquery.easing.php
		$.easing.elasout = function(x, t, b, c, d) {
			var s=1.70158;var p=0;var a=c;
			if (t==0) return b;  if ((t/=d)==1) return b+c;  if (!p) p=d*.3;
			if (a < Math.abs(c)) { a=c; var s=p/4; }
			else var s = p/(2*Math.PI) * Math.asin (c/a);
			return a*Math.pow(2,-10*t) * Math.sin( (t*d-s)*(2*Math.PI)/p ) + c + b;
		};
		$('a.back').click(function() {
			$(this).parents('div.pane').scrollTo(0, 800, { queue:true });
			$(this).parents('div.section').find('span.message').text( this.title);
			return false;
		});
		//just for the example, to stop the click on the links.
		$('ul.links').click(function(e){
			var link = e.target;
			if (link.target === '_blank') {
				return;
			}
			e.preventDefault();
			link.blur();
			if (link.title) {
				$(this).parent().find('span.message').text(link.title);
			}
		});
		
	// This one is important, many browsers don't reset scroll on refreshes
	// Reset all scrollable panes to (0,0)
	$('div.pane').scrollTo(0);
	// Reset the screen to (0,0)
	$.scrollTo(0);

	//MY ANCHOR (все ссылки <a> в шапке ведут к своим якорям)
	$('#bs-example-navbar-collapse-1 a').click(function() {//$.scrollTo works EXACTLY the same way, but scrolls the whole screen
		$.scrollTo(this.hash, 100, {offset: function() { return {top:-50, left:-5}; }} ); //100 - это скорость
		$(this.hash).find('span.message').text(this.title);
		return false;
	});

	$('#header_block a').click(function() {//$.scrollTo works EXACTLY the same way, but scrolls the whole screen
		$.scrollTo(this.hash, 100, {offset: function() { return {top:-50, left:-5}; }} ); //100 - это скорость
		$(this.hash).find('span.message').text(this.title);
		return false;
	});
	
	// TOC, shows how to scroll the whole window
	$('#toc a').click(function() {//$.scrollTo works EXACTLY the same way, but scrolls the whole screen
		$.scrollTo(this.hash, 1500, { easing:'elasout' });
		$(this.hash).find('span.message').text(this.title);
		return false;
	});
	
	// Target examples bindings
	var $target = $('#pane-target');
	$('#target-examples a').click(function() {
		$target.stop(true);
	});

	$('#relative-selector').click(function() {
		$target.scrollTo('li:eq(7)', 800);
	});
	$('#jquery-object').click(function() {
		$target.scrollTo($('#pane-target li:eq(14)') , 800);
	});
	$('#dom-element').click(function() {
		$target.scrollTo(document.getElementById('twenty'), 800);
	});
	$('#absolute-number').click(function() {
		$target.scrollTo(150, 800);
	});
	$('#absolute-number-hash').click(function() {
		$target.scrollTo({ top:800, left:700}, 800);
	});
	$('#absolute-px').click(function() {
		$target.scrollTo('520px', 800);
	});
	$('#absolute-px-hash').click(function() {
		$target.scrollTo({top:'110px', left:'290px'}, 800);
	});
	$('#relative-px').click(function() {
		$target.scrollTo('+=100', 500);
	});
	$('#relative-px-hash').click(function() {				
		$target.scrollTo({top:'-=100px', left:'+=100'}, 500);
	});
	$('#percentage-px').click(function() {				
		$target.scrollTo('50%', 800);
	});
	$('#percentage-px-hash').click(function() {				
		$target.scrollTo({top:'50%', left:'20%'}, 800);
	});
	
	// Settings examples bindings, they will all scroll to the same place, with different settings
	var $settings = $('#pane-settings');
	$('#settings-examples a').click(function() {
		// before each animation, reset to (0,0), skip this.
		$settings.stop(true).scrollTo(0);
	});
	
	$('#settings-no').click(function() {
		$settings.scrollTo('li:eq(15)', 1000);
	});
	$('#settings-axis').click(function() {// only scroll horizontally
		$settings.scrollTo('li:eq(15)', 1000, { axis:'x' });
	});
	$('#settings-duration').click(function() {// it's the same as specifying it in the 2nd argument
		$settings.scrollTo('li:eq(15)', { duration: 3000 });
	});
	$('#settings-easing').click(function() {
		$settings.scrollTo('li:eq(15)', 2500, { easing:'elasout' });
	});
	$('#settings-margin').click(function() {//scroll to the "outer" position of the element
		$settings.scrollTo('li:eq(15)', 1000, { margin:true });
	});
	$('#settings-offset').click(function() {//same as { top:-50, left:-50 }
		$settings.scrollTo('li:eq(15)', 1000, { offset:-50 });
	});
	$('#settings-offset-hash').click(function() {
		$settings.scrollTo('li:eq(15)', 1000, { offset:{ top:-5, left:-30 } });
	});
	$('#settings-offset-function').click(function() {
		$settings.scrollTo('li:eq(15)', 1000, {offset: function() { return {top:-30, left:-5}; }});
	});
	$('#settings-over').click(function() {//same as { top:-50, left:-50 }
		$settings.scrollTo('li:eq(15)', 1000, { over:0.5 });
	});
	$('#settings-over-hash').click(function() {
		$settings.scrollTo('li:eq(15)', 1000, { over:{ top:0.2, left:-0.5 } });
	});
	$('#settings-interrupt').click(function() {
		$settings.scrollTo('li:eq(15)', 10000, { interrupt:true });
	});
	$('#settings-queue').click(function() {//in this case, having 'axis' as 'xy' or 'yx' matters.
		$settings.scrollTo('li:eq(15)', 2000, { queue:true });
	});
	$('#settings-onAfter').click(function() {
		$settings.scrollTo('li:eq(15)', 2000, { 
			onAfter:function() {
				$('#settings-message').text('Got there!');
			}
		});
	});
	$('#settings-onAfterFirst').click(function() {//onAfterFirst exists only when queuing
		$settings.scrollTo('li:eq(15)', 1600, { 
			queue:true,
			onAfterFirst:function() {
				$('#settings-message').text('Got there horizontally!');
			},
			onAfter:function() {
				$('#settings-message').text('Got there vertically!');
			}
		});
	});
	$('#settings-step').click(function() {
		$settings.scrollTo('li:eq(15)', 2000, {step:function(now) {
			$('#settings-message').text(now.toFixed(2));
		}});
	});
	$('#settings-progress').click(function() {
		$settings.scrollTo('li:eq(15)', 2000, {progress:function(_, now) {
			$('#settings-message').text(Math.round(now*100) + '%');
		}});
	});
	$('#settings-fail').click(function() {
		$settings.scrollTo('li:eq(15)', 10000, {interrupt:true, fail:function() {
			$('#settings-message').text('Scroll interrupted!');
		}});
	});
});