////const { target } = require("modernizr");

function RemoveClick() {
    var boxes = [...$('.TCB')];
    var selectedIDs = boxes.filter(x => x.checked).map(x => parseInt(x.id));

    if (selectedIDs.length > 0) {
        location.href = '/HW/remove?IDsStr=' + selectedIDs.join(',') + '&isGoBack=true';
        // get array on server
        //selectedIDs.map((x, i) => (i == 0 ? "?" : "&") + 'arr=' + x.toString()).join('');
    }
}

function OnTableRowClick() {
    event.stopPropagation();
    
    var row = event.target.closest('tr');
    var checkBox = row.getElementsByClassName('TCB')[0];
    var flag = (checkBox.checked = !checkBox.checked);
    row.style.background = flag ? '#8fe37b' : '#ebebeb';
}

function EditClick() {
    var boxes = [...$('.TCB')];
    var selectedIDs = boxes.filter(x => x.checked).map(x => parseInt(x.id));

    if (selectedIDs.length > 0)
        location.href = '/HW/Edit?id=' + selectedIDs[0];
}

function DetailClick() {
    var boxes = [...$('.TCB')];
    var selectedIDs = boxes.filter(x => x.checked).map(x => parseInt(x.id));

    if (selectedIDs.length > 0)
        location.href = '/HW/Details?id=' + selectedIDs[0];
}


function OnImageChanged() {
    document.getElementById('divImageViewer').hidden = false;

    var output = document.getElementById('ViewerImage');
    output.src = URL.createObjectURL(event.target.files[0]);
    document.getElementById('CarFileImgInput').style.display = 'none';
}
function OnDivImageViewerClick() {
    document.getElementById('CarFileImgInput').click();
}


var currentSortedCol = -1;
var sortDirection = true;
function OnCarTableHeadClick() {
    var headCells = event.target.parentElement.querySelectorAll('th');

    forLabel:
    for (var i = 1; i < headCells.length; i++) {
        if (headCells[i] == event.target) {
            var rows = [...event.target.closest('table').getElementsByTagName('tbody')[0].rows];

            switch (i) {
                case 1:
                    if (currentSortedCol != 1) {
                        currentSortedCol = 1;
                        sortDirection = true;
                    }
                    rows.sort(pred);
                    break;
                case 2:
                    if (currentSortedCol != 2) {
                        currentSortedCol = 2;
                        sortDirection = true;
                    }
                    rows.sort(pred);
                    break;
                case 3:
                    if (currentSortedCol != 3) {
                        currentSortedCol = 3;
                        sortDirection = true;
                    }
                    rows.sort(pred);
                   break;
                case 4:
                    if (currentSortedCol != 4) {
                        currentSortedCol = 4;
                        sortDirection = true;
                    }
                    rows.sort(pred);
                   break;
                case 5:
                    if (currentSortedCol != 5) {
                        currentSortedCol = 5;
                        sortDirection = true;
                    }
                    rows.sort(pred);
                   break;
                case 6:
                    if (currentSortedCol != 6) {
                        currentSortedCol = 6;
                        sortDirection = true;
                    }
                    rows.sort(pred);
                   break;
                default: break forLabel;
            }

            sortDirection = !sortDirection;

            var body = event.target.closest('table').getElementsByTagName('tbody')[0];
            body.innerHTML = '';
            rows.forEach(x => body.appendChild(x));

            break;
        }
    }
}

function pred(a, b) {
    var _a = a.querySelectorAll('td')[currentSortedCol].innerText;
    var _b = b.querySelectorAll('td')[currentSortedCol].innerText;

    if (!isNaN(_a) && !isNaN(_b)) {
        _a = parseInt(_a);
        _b = parseInt(_b);
    }

    return _a > _b ? (sortDirection ? 1 : -1) : _a < _b ? (sortDirection? -1 : 1) : 0;
}
