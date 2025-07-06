function OrderItemChange() {
	var id = event.target.id;
	[...$('.OrderItemCheckbox:checked')]
		.filter(x => x.id != id)
		.forEach(x => x.checked = false);
}

function OnCategoryListChange() {
	event.target.blur();

	var data = new FormData();
	data.append('categoryID', event.target.value)

	$.ajax({
		url: $(event.target).attr('url'),
		type: "POST",
		processData: false,
		contentType: false,
		data: data,
		success: function (data) {
			var table = $('.MainTableBody');
			table.html('');

			if (data?.orders) {
				try {
					table.html(data.orders.map(order => `
<div class="TableOrderItem">
	<input value="${order.id}" id="order${order.id}" type="checkbox" class="OrderItemCheckbox" onchange="OrderItemChange()">
	<label class="OrderItemLabel" for="order${order.id}">
		<div class="MainTableBodyCell">${order.category}</div>
		<div class="MainTableBodyCell">${order.client}</div>
		<div class="MainTableBodyCell">${order.salary}</div>
		<div class="MainTableBodyCell">${order.startDate}</div>
		<div class="MainTableBodyCell">${order.endDate}</div>
	</label>
	<div class="OrderItemData">
		<div class="OrderItemDataInnerDiv">
			<div class="OrderItemDataHeader">
				<p>Brand: <span>${order.brand}</span></p>
				<p>Model: <span>${order.model}</span></p>
				<p>Year issue: <span>${order.yearIssue}</span></p>
			</div>
		</div>
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



var currentSortColumnn = 1;
var currentSortDirection = true;

function OnTableHeadCellClick() {
	var col = $(event.target).attr('sortcolumn');
	if (col == currentSortColumnn) {
		currentSortDirection = !currentSortDirection;
	}
	else {
		currentSortColumnn = col;
		currentSortDirection = true;
	}

	var data = new FormData();
	data.append('categoryID', $('.MainCategoryFilterList').val());
	data.append('column', currentSortColumnn);
	data.append('direction', currentSortDirection);

	$.ajax({
		url: $('.MainTable').attr('sortlink'),
		type: "POST",
		processData: false,
		contentType: false,
		data: data,
		success: function (data) {
			var table = $('.MainTableBody');
			table.html('');

			if (data) {
				try {
					table.html(data.map(order => `
<div class="TableOrderItem">
	<input value="${order.id}" id="order${order.id}" type="checkbox" class="OrderItemCheckbox" onchange="OrderItemChange()">
	<label class="OrderItemLabel" for="order${order.id}">
		<div class="MainTableBodyCell">${order.category}</div>
		<div class="MainTableBodyCell">${order.client}</div>
		<div class="MainTableBodyCell">${order.salary}</div>
		<div class="MainTableBodyCell">${order.startDate}</div>
		<div class="MainTableBodyCell">${order.endDate}</div>
	</label>
	<div class="OrderItemData">
		<div class="OrderItemDataInnerDiv">
			<div class="OrderItemDataHeader">
				<p>Brand: <span>${order.brand}</span></p>
				<p>Model: <span>${order.model}</span></p>
				<p>Year issue: <span>${order.yearIssue}</span></p>
			</div>
		</div>
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


