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

var bottom = document.layers.getByName("Bottom");
var middle2 = document.layers.getByName("Middle2");
var top = document.layers.getByName("Top");

bottom.visible = true;
middle2.visible = false;
top.visible = false;
exportFile("spinner-bottom@2x", 1190, 1190);
exportFile("spinner-bottom", 1190, 1190);
bottom.visible = false;
middle2.visible = true;
exportFile("spinner-middle2@2x", 1190, 1190);
exportFile("spinner-middle2", 1190, 1190);
middle2.visible = false;
top.visible = true;
exportFile("spinner-top@2x", 1190, 1190);
exportFile("spinner-top", 1190, 1190);
