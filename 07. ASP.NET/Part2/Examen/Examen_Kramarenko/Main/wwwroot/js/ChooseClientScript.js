function OnChooseButtonClick(id) {
	event.target.blur();
	if (isNaN(id)) return;

	var data = new FormData();
	data.append('clientID', id)

	$.ajax({
		url: $('.ClientTable').attr('saveurl'),
		type: "POST",
		headers: {
			"RequestVerificationToken": $('.ClientTable').attr('token')
		},
		processData: false,
		contentType: false,
		data: data,
		success: function (data) {
			window.location = $('.ClientTable').attr('backurl');
		},
		error: function (err) {
			console.log(err);
		}
	});
}

function OnBackButtonClick() {
	event.target.blur();
	window.location = $('.ClientTable').attr('backurl');
}

var currentSortColumnn = 1;
var currentSortDirection = true;

function OnTableHeadCellClick(col) {
	if (isNaN(col)) return;
	if (col == currentSortColumnn) {
		currentSortDirection = !currentSortDirection;
	}
	else {
		currentSortColumnn = col;
		currentSortDirection = true;
	}

	var data = new FormData();
	data.append('column', currentSortColumnn);
	data.append('direction', currentSortDirection);

	$.ajax({
		url: $('.ClientTable').attr('sorturl'),
		type: "POST",
		processData: false,
		contentType: false,
		data: data,
		success: function (data) {
			var table = $('.ClientTable > tbody');
			table.html('');

			if (data?.items) {
				try {
					table.html(data.items.map(c => `
<tr>
	<td>${c.client}</td>
	<td>${c.address}</td>
	<td>${c.birthday}</td>
	<td class="ClientTableCell Button"><button class="ChooseButton" onclick="OnChooseButtonClick('${c.id}')">Выбрать</button></td>
</tr>`).join('\n'));
				} catch (e) { }
			}
		},
		error: function (err) {
			console.log(err);
		}
	});
}