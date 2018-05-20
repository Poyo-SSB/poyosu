var document = app.activeDocument;

function exportFile(name, width, height) {
    var exportOptions = new ExportOptionsPNG24();
    exportOptions.matte = false;
    exportOptions.artBoardClipping = true;
    exportOptions.horizontalScale = (width / document.width) * 100;
    exportOptions.verticalScale = (height / document.height) * 100;
    
    var path = document.path + "/" + name + ".png"
    
    document.exportFile(new File(path), ExportType.PNG24, exportOptions);
}

document.pathItems.getByName("Followpoint").hidden = true;

const hidden = 25;

for (var i = 0; i < hidden; i++) {
    exportFile("followpoint-" + i, 1, 1);
}

document.pathItems.getByName("Followpoint").hidden = false;

for (var i = hidden; i < 60; i++) {
    exportFile("followpoint-" + i, 128, 50);
}