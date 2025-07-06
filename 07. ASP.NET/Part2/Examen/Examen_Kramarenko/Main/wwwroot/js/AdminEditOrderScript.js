function OnChooseProductClick(locationUrl) {
	event.preventDefault();
	window.location = locationUrl;
}

function OnChooseClientClick(locationUrl) {
	event.preventDefault();
	window.location = locationUrl;
}

function OnChooseMasterClick(locationUrl) {
	event.preventDefault();
	window.location = locationUrl;
}

function OnOrderFormSubmit() {
	event.preventDefault();

	var data = new FormData();
	data.append('OrderID', $('#EditOrderID').val());
	data.append('ProductID', $('#ProductIDValue').val());
	data.append('ClientID', $('#ClientIDValue').val());
	data.append('MasterID', $('#MasterIDValue').val());
	data.append('Salary', $('#Salary').val());

	$.ajax({
		url: event.target.action,
		method: 'POST',
		processData: false,
		contentType: false,
		data: data,
		headers: {
			"RequestVerificationToken": $('#AntiForgeryToken').val()
		},
		success: function (data) {
			$('.FormGroupInput').removeClass('Error'); // inputs
			$('#FormError').text(''); // apsn form error
			$('.FormGroupError').text(''); // span error

			if (data.url) {
				window.location = data.url;
			}
			else if (data.errors) {
				data.errors.forEach(err => {
					if (err.key.length == 0) {
						$('#FormError').text(err.value);
					}
					else {
						var errText = $('#' + err.key + 'Error'); // span error
						if (errText.length > 0) {
							errText.text(err.value);
							$('#' + err.key).addClass('Error'); // input
						}
						else {
							$('#FormError').text(err.value);
						}
					}
				});
			}
		},
		error: function (error) {
			console.log(error);
		}
	});
}
