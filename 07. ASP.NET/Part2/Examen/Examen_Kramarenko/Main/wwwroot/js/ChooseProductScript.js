function OnChooseButtonClick(id) {
    event.target.blur();
    if (isNaN(id)) return;

    var data = new FormData();
    data.append('productID', id)

    $.ajax({
        url: $('.ProductTable').attr('saveurl'),
        type: "POST",
        headers: {
            "RequestVerificationToken": $('.ProductTable').attr('token')
        },
        processData: false,
        contentType: false,
        data: data,
        success: function (data) {
            window.location = $('.ProductTable').attr('backurl');
        },
        error: function (err) {
            console.log(err);
        }
    });
}

function OnBackButtonClick() {
    event.target.blur();
    window.location = $('.ProductTable').attr('backurl');
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
		url: $('.ProductTable').attr('sorturl'),
		type: "POST",
		processData: false,
		contentType: false,
		data: data,
		success: function (data) {
			var table = $('.ProductTable > tbody');
			table.html('');

			if (data?.items) {
				try {
					table.html(data.items.map(p => `
<tr>
	<td>${p.product}</td>
	<td>${p.year}</td>
	<td>${p.price}</td>
	<td class="SerialNumber">#${p.snum}</td>
	<td class="ProductTableCell Button"><button class="ChooseButton" onclick="OnChooseButtonClick('${p.id}')">Выбрать</button></td>
</tr>`).join('\n'));
				} catch (e) { }
			}
		},
		error: function (err) {
			console.log(err);
		}
	});
}