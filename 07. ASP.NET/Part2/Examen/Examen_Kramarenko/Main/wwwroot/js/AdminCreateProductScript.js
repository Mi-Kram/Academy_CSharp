function OnFormSubmit() {
	event.preventDefault();

    var data = new FormData();
    data.append("CategoryID", $('#CategoryID').val() || '');
    data.append("SerialNumber", $('#SerialNumber').val());
    data.append("Price", $('#Price').val());
    data.append("YearIssue", $('#YearIssue').val());
    data.append("Brand", $('#Brand').val());
    data.append("Model", $('#Model').val());

    $.ajax({
        url: $(event.target).attr('action'),
        type: "POST",
        headers: {
            "RequestVerificationToken": $('#AntiForgeryToken').val()
        },
        processData: false,
        contentType: false,
        data: data,
        success: function (data) {
            $('.FormGroupInput').removeClass('Error'); // input
            $('#FormError').text(''); // form span error
            $('.FormGroupError').text(''); // span

            if (data?.url) {
                window.location = data.url;
            }
            else if (data?.errors) {
                data.errors.forEach(err => {
                    if (err.key.length == 0) {
                        $('#FormError').text(err.value); // form span
                    }
                    else {
                        var errText = $('#' + err.key + 'Error'); // span
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
        error: function (err) {
            console.log(err);
        }
    });
}