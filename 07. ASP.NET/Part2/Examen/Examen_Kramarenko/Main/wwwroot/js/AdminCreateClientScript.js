function OnFormSubmit() {
    event.preventDefault();

    var data = new FormData();
    data.append("FirstName", $('#FirstName').val());
    data.append("LastName", $('#LastName').val());
    data.append("Address", $('#Address').val());
    data.append("PassportID", $('#PassportID').val());
    data.append("Birthday", $('#Birthday').val() || '');

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