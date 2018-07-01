var document = app.activeDocument;

function exportFile(name, width, height) {
    var exportOptions = new ExportOptionsPNG24();
    exportOptions.matte = false;
    exportOptions.artBoardClipping = true;
    exportOptions.horizontalScale = 100 * width / document.width;
    exportOptions.verticalScale = 100 * height / document.height;
    
    var path = document.path + "/" + name + ".png"
    
    document.exportFile(new File(path), ExportType.PNG24, exportOptions);
}

var followpoint = document.pathItems.getByName("Followpoint");

for (var i = 0; i < 52; i++) {
    followpoint.opacity = alphaCurve(i);
    
    if (followpoint.opacity != 0) {
        exportFile("../shared/followpoint-" + i, 128, 50);
    } else {
        exportFile("../shared/followpoint-" + i, 1, 1);
    }
}


function alphaCurve(t) {
    if (t <= 24) {
        return 0;
    } else if (t <= 30) {
        return 100 * (t - 24) / 6;
    } else if (t <= 45) {
        return 100;
    } else if (t <= 51) {
        return 100 - 100 * (t - 45) / 6;
    } else {
        return 0;
    }
}