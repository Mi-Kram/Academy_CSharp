function OnOrderTableMainCheckboxChange() {
	$('.OrderCheckBox').prop('checked', event.target.checked);
	var sum = [...$('.OrderCheckBox:checked')]
		.map(x => $('#' + x.id).closest('tr').find('.SalaryCell').html())
		.map(x => isNaN(x) ? 0 : parseFloat(x))
		.reduce((x, sum) => x + sum, 0);
	$('#SalarySum').text(sum);
}

function OnOrderTableRowChange() {
	var sum = [...$('.OrderCheckBox:checked')]
		.map(x => $('#' + x.id).closest('tr').find('.SalaryCell').html())
		.map(x => isNaN(x) ? 0 : parseFloat(x))
		.reduce((x, sum) => x + sum, 0);
	$('#SalarySum').text(sum);

	if (event.target.checked) {
		if ($('.OrderCheckBox:not(:checked)').length == 0)
			$('#OrderSelectAllCheckBox').prop('checked', true);
	}
	else {
		if ($('#OrderSelectAllCheckBox').prop('checked'))
			$('#OrderSelectAllCheckBox').prop('checked', false);
	}
}

var currentSortColumnn = 1;
var currentSortDirection = true;

function OnOrderTableHeadCellClick(col) {
	if (isNaN(col)) return;
	if (col == currentSortColumnn) {
		currentSortDirection = !currentSortDirection;
	}
	else {
		currentSortColumnn = col;
		currentSortDirection = true;
	}
	$('#OrderSelectAllCheckBox').prop('checked', false);

	var data = new FormData();
	data.append('masterID', $('.MasterFilterList').val());
	data.append('column', currentSortColumnn);
	data.append('direction', currentSortDirection);

	$.ajax({
		url: $('.OrderTable').attr('sorturl'),
		type: "POST",
		processData: false,
		contentType: false,
		data: data,
		success: function (data) {
			var table = $('.OrderTable > tbody');
			table.html('');
			$('#OrderSelectAllCheckBox').prop('checked', false);
			$('#SalarySum').text('0');

			if (data?.orders) {
				try {
					table.html(data.orders.map(x => `
<tr>
	<td><div class="OrderTableCheckCell"><input id="${x.id}" onchange="OnOrderTableRowChange()" class="OrderCheckBox" type="checkbox"></div></td>
	<td>${x.product}</td>
	<td>${x.client}</td>
	<td>${x.master}</td>
	<td class="SalaryCell">${x.salary}</td>
	<td>${x.startDate}</td>
	<td class="EndDateCell${x.isDone ? '' : ' Not'}">${x.endDate}</td>
</tr>`).join('\n'));
				} catch (e) { }
			}
		},
		error: function (err) {
			console.log(err);
		}
	});
}

function OnOrderDeleteClick() {
	event.target.blur();

	var ids = [...$('.OrderCheckBox:checked')].map(x => x.id);
	if (ids.length > 0) {
		if (confirm('Вы действительно хотите удалить выделенные записи?')) {
			var data = new FormData();
			ids.forEach(x => data.append('orderIDs', x));

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
							$('#OrderSelectAllCheckBox').prop('checked', false);
							$('#SalarySum').text('0');
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

function OnOrderEditCkick() {
	event.target.blur();

	var ids = [...$('.OrderCheckBox:checked')].map(x => x.id);
	if (ids.length > 0) {
		var id = ids[0];
		window.location = $(event.target).attr('url') + '?orderID=' + id;
	}
}

function OnMasterListChange() {
	event.target.blur();
	currentSortColumnn = 1;
	currentSortDirection = true;

	var data = new FormData();
	data.append('masterID', event.target.value)

	$.ajax({
	  url: $(event.target).attr('url'),
	  type: "POST",
	  processData: false,
	  contentType: false,
	  data: data,
		  success: function (data) {
			  var table = $('.OrderTable > tbody');
			  table.html('');
			  $('#OrderSelectAllCheckBox').prop('checked', false);
			  $('#SalarySum').text('0');

			  if (data?.orders) {
				  try {
					  table.html(data.orders.map(x => `
<tr>
	<td><div class="OrderTableCheckCell"><input id="${x.id}" onchange="OnOrderTableRowChange()" class="OrderCheckBox" type="checkbox"></div></td>
	<td>${x.product}</td>
	<td>${x.client}</td>
	<td>${x.master}</td>
	<td class="SalaryCell">${x.salary}</td>
	<td>${x.startDate}</td>
	<td class="EndDateCell${x.isDone ? '' : ' Not'}">${x.endDate}</td>
</tr>`).join('\n'));
				  } catch (e) { }
			  }
		  },
	  error: function (err) {
		console.log(err);
	  }
	});
}
