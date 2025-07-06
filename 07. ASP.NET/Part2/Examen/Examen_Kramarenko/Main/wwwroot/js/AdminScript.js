var MenuItemOrdersClicks = 0;
function OnMenuItemOrdersFocus() {
	var children = $('.MenuItemDropDownList > *').length;
	$('.MenuItemDropDownList').animate({
		height: `${children * 50}px`,
		paddingTop: '3px'
	},
		{
			duration: 200,
			fill: 'forwards'
		});
}

function OnMenuItemOrdersBlur() {
	MenuItemOrdersClicks = 0;
	$('.MenuItemDropDownList').animate({
		height: '0px',
		paddingTop: '0px'
	},
		{
			duration: 200,
			fill: 'forwards'
		});
}

function OnMenuItemOrdersClick() {
	if (++MenuItemOrdersClicks >= 2) {
		event.target.blur();
	}
}