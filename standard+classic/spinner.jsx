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

var bottom = document.layers.getByName("Bottom");
var middle2 = document.layers.getByName("Middle2");

bottom.visible = true;
middle2.visible = false;
exportFile("spinner-bottom@2x", 1190, 1190);
exportFile("spinner-bottom", 1190, 1190);
bottom.visible = false;
middle2.visible = true;
exportFile("spinner-middle2@2x", 1190, 1190);
exportFile("spinner-middle2", 1190, 1190);