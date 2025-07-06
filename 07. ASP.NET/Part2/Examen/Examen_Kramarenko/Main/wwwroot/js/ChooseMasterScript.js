function OnChooseButtonClick(id) {
	event.target.blur();
	if (!id) return;

	var data = new FormData();
	data.append('masterID', id)

	$.ajax({
		url: $('.MasterTable').attr('saveurl'),
		type: "POST",
		headers: {
			"RequestVerificationToken": $('.MasterTable').attr('token')
		},
		processData: false,
		contentType: false,
		data: data,
		success: function (data) {
			window.location = $('.MasterTable').attr('backurl');
		},
		error: function (err) {
			console.log(err);
		}
	});
}

function OnBackButtonClick() {
	event.target.blur();
	window.location = $('.MasterTable').attr('backurl');
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
		url: $('.MasterTable').attr('sorturl'),
		type: "POST",
		processData: false,
		contentType: false,
		data: data,
		success: function (data) {
			var table = $('.MasterTable > tbody');
			table.html('');

			if (data?.items) {
				try {
					table.html(data.items.map(m => `
<tr>
	<td class="MasterTableCell Photo"><div><img src="${m.photo}" alt="photo" /></div></td>
	<td>${m.master}</td>
	<td class="MasterTableCell Button"><button class="ChooseButton" onclick="OnChooseButtonClick('${m.id}')">Выбрать</button></td>
</tr>`).join('\n'));
				} catch (e) { }
			}
		},
		error: function (err) {
			console.log(err);
		}
	});
}