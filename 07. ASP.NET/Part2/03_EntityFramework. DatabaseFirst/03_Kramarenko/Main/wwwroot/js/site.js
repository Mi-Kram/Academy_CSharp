// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function OnInputChanged() {
	var height = 0;

	if (event.target.checked) {
		var cnt = $("#" + event.target.id + " ~ .BooksGroupContent").children().length;
		height = cnt * 50;
	}

	$("#" + event.target.id + " ~ .BooksGroupContent").animate({
		height: height + "px"
	},
	{
		duration: 500,
		fill: 'forwards'
	});
}




