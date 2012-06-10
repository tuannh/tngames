/**
	Hover Accordion Menu
	A jQuery menu accordion plugin that works on roll over instead of the 
	typical mouse click accordion. What sets this plugin appart is that it 
	automatically retrieves back on mouse leave. 
	
	Hover Accordion Menu uses Brian's Hover Intent to determine the best
	mouse over and mouse out produced by the user.
	
	Further features will be added in future realeases, including, option to 
	change from on mouseover/mouseout to on mouseclick, and other options.
	
	Author: Samuel Roldan <http://www.sam3k.com/blog>
	
	Version: 1.0
	
	Dependencies:
	- jquery 1.2.6 <http://docs.jquery.com/Release:jQuery_1.2.6>
	- hoverIntent r5 <http://cherne.net/brian/resources/jquery.hoverIntent.html>
*/
(function($) {
	$.fn.hoverAccordionMenu = function(options) {
		
		var hoverIntentConfig = {    
			 sensitivity: 1, // number = sensitivity threshold (must be 1 or higher)    
			 interval: 50, // number = milliseconds for onMouseOver polling interval    
			 over: slideDown, // function = onMouseOver callback (REQUIRED)    
			 timeout: 200, // number = milliseconds delay before onMouseOut    
			 out: slideUp // function = onMouseOut callback (REQUIRED)    
		};

$('ul.adMenu > li').not(".active").find("ul").hide(); //hide 2nd level at startup
	
		/*2nd approach with set time out on both mouseover and mouseout*/
$('ul.adMenu > li:has(ul > li)').not(".active").hoverIntent(hoverIntentConfig);
		
		function slideDown() { 
			$(this).find("ul").slideDown("normal");
		}
		
		function slideUp() {
			$(this).find("ul").slideUp("slow"); 
		}
	}
})(jQuery);