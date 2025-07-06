function OnClientTableMainCheckboxChange() {
	$('.ClientCheckBox').prop('checked', event.target.checked);
}

function OnClientTableRowChange() {
	if (event.target.checked) {
		if ($('.ClientCheckBox:not(:checked)').length == 0)
			$('#ClientSelectAllCheckBox').prop('checked', true);
	}
	else {
		if ($('#ClientSelectAllCheckBox').prop('checked'))
			$('#ClientSelectAllCheckBox').prop('checked', false);
	}
}

var currentSortColumnn = 1;
var currentSortDirection = true;

function OnClientTableHeadCellClick(col) {
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
			$('#ClientSelectAllCheckBox').prop('checked', false);

			if (data?.items) {
				try {
					table.html(data.items.map(c => `
<tr>
	<td><div class="ClientTableCheckCell"><input id="${c.id}" onchange="OnClientTableRowChange()" class="ClientCheckBox" type="checkbox"></div></td>
	<td>${c.client}</td>
	<td>${c.address}</td>
	<td>${c.birthday}</td>
</tr>`).join('\n'));
				} catch (e) { }
			}
		},
		error: function (err) {
			console.log(err);
		}
	});
}

function OnClientDeleteClick() {
	event.target.blur();

	var ids = [...$('.ClientCheckBox:checked')].map(x => x.id);
	if (ids.length > 0) {
		if (confirm('Вы действительно хотите удалить выделенные записи?')) {
			var data = new FormData();
			ids.forEach(x => data.append('clientIDs', x));

			$.ajax({
				url: $(event.target).attr('url'),
				type: "POST",
				processData: false,
				contentType: false,
				data: data,
				headers: {
					"RequestVerificationToken": $(event.target).attr('token')
				},
				success: function (data) {
					if (data) {
						try {
							data.forEach(x => {
								$('#' + x).closest('tr').remove();
							});
							$('#ClientSelectAllCheckBox').prop('checked', false);
						} catch (e) { }
					}
				},
				error: function (err) {
					console.log(err);
				}
			});
		}
	}
}

function OnClientEditCkick() {
	event.target.blur();

	var ids = [...$('.ClientCheckBox:checked')].map(x => x.id);
	if (ids.length > 0) {
		var id = ids[0];
		window.location = $(event.target).attr('url') + '?id=' + id;
	}
}
