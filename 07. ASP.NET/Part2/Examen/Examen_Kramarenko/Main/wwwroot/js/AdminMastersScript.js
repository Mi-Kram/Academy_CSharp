function OnMasterTableMainCheckboxChange() {
	$('.MasterCheckBox').prop('checked', event.target.checked);
}

function OnMasterTableRowChange() {
	if (event.target.checked) {
		if ($('.MasterCheckBox:not(:checked)').length == 0)
			$('#MasterSelectAllCheckBox').prop('checked', true);
	}
	else {
		if ($('#MasterSelectAllCheckBox').prop('checked'))
			$('#MasterSelectAllCheckBox').prop('checked', false);
	}
}

var currentSortColumnn = 1;
var currentSortDirection = true;

function OnMasterTableHeadCellClick(col) {
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
			var table = $('.MasterTableBody');
			table.html('');
			$('#MasterSelectAllCheckBox').prop('checked', false);

			if (data) {
				try {
					table.html(data.map(m => `
<div class="MasterTableRow">
	<div class="MasterTableCell Check">
		<input masterid="${m.id}" onchange="OnMasterTableRowChange()" type="checkbox" class="MasterCheckBox">
	</div>
	<div class="MasterTableCell Photo">
		<div class="MasterPhotoDiv">
			<img src="${m.photo}" alt="photo">
		</div>
	</div>
	<div class="MasterTableCell UserName">
		<p class="MasterUserName">${m.username}</p>
	</div>
	<div class="MasterTableCell Done">
		<p class="MasterCompletedWorks">${m.done}</p>
	</div>
	<div class="MasterTableCell NotDone">
		<p class="MasterNotCompletedWorks">${m.notdone}</p>
	</div>
</div>`).join('\n'));
				} catch (e) { }
			}
		},
		error: function (err) {
			console.log(err);
		}
	});
}

function OnMasterDeleteClick() {
	event.target.blur();

	var ids = [...$('.MasterCheckBox:checked')].map(x => x.id);
	if (ids.length > 0) {
		if (confirm('Вы действительно хотите удалить выделенные записи?')) {
			var data = new FormData();
			ids.forEach(x => data.append('masterIDs', x));

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
								$('#' + x).closest('.MasterTableRow').remove();
							});
							$('#MasterSelectAllCheckBox').prop('checked', false);
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

function OnMasterEditCkick() {
	event.target.blur();

	var ids = [...$('.MasterCheckBox:checked')].map(x => x.id);
	if (ids.length > 0) {
		var id = ids[0];
		window.location = $(event.target).attr('url') + '?id=' + id;
	}
}





