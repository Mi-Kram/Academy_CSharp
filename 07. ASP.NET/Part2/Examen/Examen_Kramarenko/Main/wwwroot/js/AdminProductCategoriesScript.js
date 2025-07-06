function OnCategoryTableMainCheckboxChange() {
	$('.CategoryCheckBox').prop('checked', event.target.checked);
}

function OnCategoryTableRowChange() {
	if (event.target.checked) {
		if ($('.CategoryCheckBox:not(:checked)').length == 0)
			$('#CategorySelectAllCheckBox').prop('checked', true);
	}
	else {
		if ($('#CategorySelectAllCheckBox').prop('checked'))
			$('#CategorySelectAllCheckBox').prop('checked', false);
	}
}

function OnCategoryDeleteClick() {
	event.target.blur();

	var ids = [...$('.CategoryCheckBox:checked')].map(x => x.id);
	if (ids.length > 0) {
		if (confirm('Вы действительно хотите удалить выделенные записи?')) {
			var data = new FormData();
			ids.forEach(x => data.append('categoryIDs', x));

			$.ajax({
				url: $('.CRUDButton.Delete').attr('url'),
				type: "POST",
				processData: false,
				contentType: false,
				data: data,
				headers: {
					"RequestVerificationToken": $('#AntiForgeryToken').val()
				},
				success: function (data) {
					if (data) {
						try {
							data.forEach(x => {
								$('#' + x).closest('.CategoryTableRow').remove();
							});
							$('#CategorySelectAllCheckBox').prop('checked', false);
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

function ShowCategoryForm(id, category) {
	$('#CategoryValue').val(category);
	$('#CategoryValue').removeClass('Error');
	$('#CategoryValueError').text('');
	$('#CategoryID').val(id);

	$('.CategoryWrapper').addClass('Active');
}

function HideCategoryForm() {
	$('#CategoryValue').val('');
	$('#CategoryValue').removeClass('Error');
	$('#CategoryValueError').text('');
	$('#CategoryID').val('');

	$('.CategoryWrapper').removeClass('Active');
}

function OnAddCategoryClick() {
	event.target.blur();
	$('.CategoryFormTitle').text('Создание категории');
	$('.FormButton.Submit').text('Создать');

	ShowCategoryForm('', '');
}

function OnCategoryEditCkick() {
	event.target.blur();

	var ids = [...$('.CategoryCheckBox:checked')].map(x => x.id);
	if (ids.length > 0) {
		var id = ids[0];
		$('.CategoryFormTitle').text('Изменение категории');
		$('.FormButton.Submit').text('Изменить');

		ShowCategoryForm(id, $(`.CategoryValue[categoryid=${id}]`).text())
	}
}

function OnCategoryFormSubmit() {
	event.preventDefault();

	var selector = '';
	var data = new FormData();
	data.append('Value', $('#CategoryValue').val());

	var id = $('#CategoryID').val();

	if (id?.length > 0) {
		selector = '.CRUDButton.Edit';
		data.append('ID', id);
	}
	else {
		selector = '.CRUDButton.Create';
	}

	$.ajax({
		url: $(selector).attr('url'),
		type: "POST",
		processData: false,
		contentType: false,
		data: data,
		headers: {
			"RequestVerificationToken": $('#AntiForgeryToken').val()
		},
		success: function (data) {
			$('.CategoryInput').removeClass('Error'); // inputs
			$('.CategoryError').text(''); // span error

			if (data) {
				if (data.errors) {
					data.errors.forEach(err => {
						if (err.key.length > 0) {
							var errText = $('#Category' + err.key + 'Error'); // span error
							if (errText.length > 0) {
								errText.text(err.value);
								$('#Category' + err.key).addClass('Error'); // input
							}
						}
					});
				}
				else if (data.items) {
					$('#CategorySelectAllCheckBox').prop('checked', false);
					var lst = $('.CategoryTableBody');
					lst.html('');
					HideCategoryForm();

					try {
						lst.html(data.items.map(x => `
<div class="CategoryTableRow">
	<div class="CategoryTableCell Check">
		<input id="${x.id}" onchange="OnCategoryTableRowChange()" type="checkbox" class="CategoryCheckBox">
	</div>
	<div class="CategoryTableCell Category">
		<p class="CategoryValue" categoryid="${x.id}">${x.value}</p>
	</div>
</div>`).join('\n'));
					} catch (e) { }
				}
			}
		},
		error: function (err) {
			console.log(err);
		}
	});
}

function OnCategoryFormCancel() {
	HideCategoryForm();
}
