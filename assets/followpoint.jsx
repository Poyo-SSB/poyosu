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

document.pathItems.getByName("Followpoint").hidden = true;

const hidden = 25;

for (var i = 0; i < hidden; i++) {
    exportFile("../shared/followpoint-" + i, 1, 1);
}

document.pathItems.getByName("Followpoint").hidden = false;

for (var i = hidden; i < 60; i++) {
    exportFile("../shared/followpoint-" + i, 128, 50);
}