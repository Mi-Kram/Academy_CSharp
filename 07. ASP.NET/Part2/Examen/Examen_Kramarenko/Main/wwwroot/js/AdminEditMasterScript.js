function TogglePasswordVisibility() {
	event.preventDefault();
	var isVisible = event.target.classList.toggle('Active');
	event.target.parentElement.getElementsByClassName('FormGroupInput')[0].type = isVisible ? 'text' : 'password';
}

function OnPhotoChanged() {
	var files = event.target.files;
	if (files?.length > 0) {
		document.getElementById('PhotoPreview').src = URL.createObjectURL(files[0]);
	}
}

function OnMasterFormSubmit() {
	event.preventDefault();

	var data = new FormData();
	data.append('Id', $('#MasterId').val());
	data.append('UserName', $('#UserName').val());
	data.append('ChangePassword', $('#ChangePassword').prop('checked'));
	data.append('NewPassword', $('#NewPassword').val());
	data.append('ConfirmNewPassword', $('#ConfirmNewPassword').val());

	var file = $('#Photo').prop('files');
	file = file.length > 0 ? file[0] : null;
	data.append('Photo', file);

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
