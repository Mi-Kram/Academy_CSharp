function OnOrderFormSubmit() {
    event.preventDefault();

    var data = new FormData();
    data.append("MasterID", $('#MasterID').val());
    data.append("ProductID", $('#ProductIDValue').val());
    data.append("ClientID", $('#ClientIDValue').val());
    data.append("Salary", $('#Salary').val());

    $.ajax({
        url: $(event.target).attr('action'),
        type: "POST",
        headers: {
            "RequestVerificationToken": $('#OrderFormAntiForgeryToken').val()
        },
        processData: false,
        contentType: false,
        data: data,
        success: function (data) {
            $('.ProductButtonChooser').removeClass('Error'); // input
            $('.FormGroupInput').removeClass('Error'); // input
            $('#FormError').text(''); // form span error
            $('.FormGroupError').text(''); // span

            if (data?.url) {
                window.location = data.url
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

    /*=========*/
    /* Product */
    /*=========*/

function ChooseProductClick() {
    event.preventDefault();

    $('#PCHeader1').prop('checked', true);
    $('.PCBody').addClass('Hidden');
    $(`.PCBody[pcheader=PCHeader1]`).removeClass('Hidden');
    $('body').css('overflow', 'hidden');
    $('.ProductChooser').css('display', 'flex');

    $('#PCCategoryID > option:not([value])').prop('selected', true);
    $('.PCFormGroupInput').val('');

    $('.PCFormGroupInput').removeClass('Error'); // input
    $('#PCFormError').text(''); // form span error
    $('.PCFormGroupError').text(''); // span

    $('#PCSearchInput').val('');
    $('.PCFoundedMsg').addClass('Hidden');
    $('#PCFoundedCount').text('0');
    $('.PC-FoundedTable tr').remove();

    $('.PC-InnerDiv').css('top', '-150%');
    $('.PC-InnerDiv').animate({
        top: '0%'
    }, {
        duration: 500,
        fill: 'forwards',
        easing: 'linear'
    });
}

function ChooserProductCloseClick() {
    $('.PC-InnerDiv').animate({
        top: '150%'
    }, {
        duration: 500,
        fill: 'forwards',
        easing: 'linear'
    });

    setTimeout(() => {
        $('.ProductChooser').css('display', 'none');
        $('body').css('overflow', 'auto');
    }, 500);
}

function OnPCHeaderChanged() {
    $('.PCBody').addClass('Hidden');
    $(`.PCBody[pcheader=${event.target.id}]`).removeClass('Hidden');
}

function PCSearchClick() {
    event.target.blur();

    var data = new FormData();
    data.append('serialNumber', $('#PCSearchInput').val());

    $.ajax({
        url: $(event.target).attr('url'),
        type: "POST",
        processData: false,
        contentType: false,
        data: data,
        success: function (data) {
            $('.PCFoundedMsg').removeClass('Hidden');
            var table = $('.PC-FoundedTable > tbody');
            table.find('tr').remove();

            if (data) {
                $('#PCFoundedCount').text(data.length || '0');
                try {
                    table.append(data.map(x => `
<tr>
    <td>${x.category}</td>
    <td>${x.brand} ${x.model}</td>
    <td>${x.yearIssue}</td>
    <td><button onclick="OnProductChooseButtonClick()" productid='${x.id}' serialnumber='${x.serialNumber}' productcategory="${x.category}" productbrand="${x.brand}" productmodel="${x.model}">Выбрать</button></td>
</tr>`).join('\n'));
                } catch (e) { }
			}
        },
        error: function (err) {
            console.log(err);
        }
    });
}

function PCClearClick() {
    $('#PCSearchInput').val('');
    $('#PCSearchInput').focus();

    $('.PCFoundedMsg').addClass('Hidden');
    $('#PCFoundedCount').text('0');
    $('.PC-FoundedTable tr').remove();
}

function OnPCFormSubmit() {
    event.preventDefault();

    var data = new FormData();
    data.append("CategoryID", $('#PCCategoryID').val() || '');
    data.append("SerialNumber", $('#PCSerialNumber').val());
    data.append("Price", $('#PCPrice').val());
    data.append("YearIssue", $('#PCYearIssue').val());
    data.append("Brand", $('#PCBrand').val());
    data.append("Model", $('#PCModel').val());

    $.ajax({
        url: $(event.target).attr('action'),
        type: "POST",
        headers: {
            "RequestVerificationToken": $('#PCFormAntiForgeryToken').val()
        },
        processData: false,
        contentType: false,
        data: data,
        success: function (data) {
            $('.PCFormGroupInput').removeClass('Error'); // input
            $('#PCFormError').text(''); // form span error
            $('.PCFormGroupError').text(''); // span

            if (data?.success) {
                ChooseProduct(data.success);
            }
            else if (data?.errors) {
                data.errors.forEach(err => {
                    if (err.key.length == 0) {
                        $('#PCFormError').text(err.value); // form span
                    }
                    else {
                        var errText = $('#PC' + err.key + 'Error'); // span
                        if (errText.length > 0) {
                            errText.text(err.value);
                            $('#PC' + err.key).addClass('Error'); // input
                        }
                        else {
                            $('#PCFormError').text(err.value);
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

function OnProductChooseButtonClick() {
    ChooseProduct({
        category: $(event.target).attr('productcategory'),
        brand: $(event.target).attr('productbrand'),
        model: $(event.target).attr('productmodel'),
        serialNumber: $(event.target).attr('serialnumber'),
        id: $(event.target).attr('productid')
    });
}

function ChooseProduct(product) {
    $('#ProductCategory').text(product.category);
    $('#ProductBrandModel').text(`${product.brand} ${product.model}`);
    $('#ProductSerialNumber').text(product.serialNumber);
    $('#ProductIDValue').val(product.id);

    $('#ProductID').removeClass('Empty');
    ChooserProductCloseClick();
}

    /*========*/
    /* Client */
    /*========*/

function ChooseClientClick() {
    event.preventDefault();

    $('#CCHeader1').prop('checked', true);
    $('.CCBody').addClass('Hidden');
    $(`.CCBody[ccheader=CCHeader1]`).removeClass('Hidden');
    $('body').css('overflow', 'hidden');
    $('.ClientChooser').css('display', 'flex');

    $('.CCFormGroupInput').val('');
    $('.CCFormGroupInput').removeClass('Error'); // input
    $('#CCFormError').text(''); // form span error
    $('.CCFormGroupError').text(''); // span

    $('#CCSearchInput').val('');
    $('.CCFoundedMsg').addClass('Hidden');
    $('#CCFoundedClientCount').text('0');
    $('.CC-FoundedClientsTable tr').remove();

    $('.CC-InnerDiv').css('top', '-150%');
    $('.CC-InnerDiv').animate({
        top: '0%'
    }, {
        duration: 500,
        fill: 'forwards',
        easing: 'linear'
    });
}

function ChooserClientCloseClick() {
    $('.CC-InnerDiv').animate({
        top: '150%'
    }, {
        duration: 500,
        fill: 'forwards',
        easing: 'linear'
    });

    setTimeout(() => {
        $('.ClientChooser').css('display', 'none');
        $('body').css('overflow', 'auto');
    }, 500);
}

function OnCCHeaderChanged() {
    $('.CCBody').addClass('Hidden');
    $(`.CCBody[ccheader=${event.target.id}]`).removeClass('Hidden');
}

function CCSearchClick() {
    event.target.blur();

    var data = new FormData();
    data.append('passportID', $('#CCSearchInput').val());

    $.ajax({
        url: $(event.target).attr('url'),
        type: "POST",
        processData: false,
        contentType: false,
        data: data,
        success: function (data) {
            $('.CCFoundedMsg').removeClass('Hidden');
            var table = $('.CC-FoundedClientsTable > tbody');
            table.find('tr').remove();

            if (data) {
                $('#CCFoundedClientCount').text(data.length || '0');
                try {
                    table.append(data.map(x => `
<tr>
    <td>${x.fName} ${x.lName}</td>
    <td>${x.birthday}</td>
    <td><button onclick="OnClientChooseButtonClick()" clientid='${x.id}' passportid='${x.passportId}' firstname="${x.fName}" lastname="${x.lName}" clientbirthday="${x.birthday}">Выбрать</button></td>
</tr>`).join('\n'));
                } catch (e) { }
            }
        },
        error: function (err) {
            console.log(err);
        }
    });
}

function OnClientChooseButtonClick() {
    ChooseClient({
        id: $(event.target).attr('clientid'),
        fName: $(event.target).attr('firstname'),
        lName: $(event.target).attr('lastname'),
        passportId: $(event.target).attr('passportid'),
        birthday: $(event.target).attr('clientbirthday'),
    });
}

function OnCCFormSubmit() {
    event.preventDefault();

    var data = new FormData();
    data.append("FirstName", $('#CCFirstName').val());
    data.append("LastName", $('#CCLastName').val());
    data.append("Address", $('#CCAddress').val());
    data.append("PassportID", $('#CCPassportID').val());
    data.append("Birthday", $('#CCBirthday').val() || '');

    $.ajax({
        url: $('.CCForm').attr('action'),
        type: "POST",
        headers: {
            "RequestVerificationToken": $('#CCFormAntiForgeryToken').val()
        },
        processData: false,
        contentType: false,
        data: data,
        success: function (data) {
            $('.CCFormGroupInput').removeClass('Error'); // input
            $('#CCFormError').text(''); // form span error
            $('.CCFormGroupError').text(''); // span

            if (data?.success) {
                ChooseClient(data.success);
            }
            else if (data?.errors) {
                data.errors.forEach(err => {
                    if (err.key.length == 0) {
                        $('#CCFormError').text(err.value); // form span
                    }
                    else {
                        var errText = $('#CC' + err.key + 'Error'); // span
                        if (errText.length > 0) {
                            errText.text(err.value);
                            $('#CC' + err.key).addClass('Error'); // input
                        }
                        else {
                            $('#CCFormError').text(err.value);
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

function ChooseClient(client) {
    $('#ClientName').text(`${client.fName} ${client.lName}`);
    $('#ClientBirthday').text(client.birthday);
    $('#ClientPassportID').text(client.passportId);
    $('#ClientIDValue').val(client.id);

    $('#ClientID').removeClass('Empty');
    ChooserClientCloseClick();
}

function CCClearClick() {
    $('#CCSearchInput').val('');
    $('#CCSearchInput').focus();

    $('.CCFoundedMsg').addClass('Hidden');
    $('#CCFoundedClientCount').text('0');
    $('.CC-FoundedClientsTable tr').remove();
}
