function OnProductTableMainCheckboxChange() {
	$('.ProductCheckBox').prop('checked', event.target.checked);
}

function OnProductTableRowChange() {
	if (event.target.checked) {
		if ($('.ProductCheckBox:not(:checked)').length == 0)
			$('#ProductSelectAllCheckBox').prop('checked', true);
	}
	else {
		if ($('#ProductSelectAllCheckBox').prop('checked'))
			$('#ProductSelectAllCheckBox').prop('checked', false);
	}
}

var currentSortColumnn = 1;
var currentSortDirection = true;

function OnProductTableHeadCellClick(col) {
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
		url: $('.ProductTable').attr('sorturl'),
		type: "POST",
		processData: false,
		contentType: false,
		data: data,
		success: function (data) {
			var table = $('.ProductTable > tbody');
			table.html('');
			$('#ProductSelectAllCheckBox').prop('checked', false);

			if (data?.items) {
				try {
					table.html(data.items.map(p => `
<tr>
	<td><div class="ProductTableCheckCell"><input id="${p.id}" onchange="OnProductTableRowChange()" class="ProductCheckBox" type="checkbox"></div></td>
	<td>${p.product}</td>
	<td>${p.price}</td>
	<td>${p.year}</td>
	<td>${p.snum}</td>
</tr>`).join('\n'));
				} catch (e) { }
			}
		},
		error: function (err) {
			console.log(err);
		}
	});
}

function OnProductDeleteClick() {
	event.target.blur();

	var ids = [...$('.ProductCheckBox:checked')].map(x => x.id);
	if (ids.length > 0) {
		if (confirm('Вы действительно хотите удалить выделенные записи?')) {
			var data = new FormData();
			ids.forEach(x => data.append('productIDs', x));

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
							$('#ProductSelectAllCheckBox').prop('checked', false);
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

function OnProductEditCkick() {
	event.target.blur();

	var ids = [...$('.ProductCheckBox:checked')].map(x => x.id);
	if (ids.length > 0) {
		var id = ids[0];
		window.location = $(event.target).attr('url') + '?id=' + id;
	}
}
