function openModal(note, cont) {
    jQuery.noConflict();
    $('#EdycjaNotatki').modal('show');
    document.getElementById("tytul").value = note;
    document.getElementById("tekst").value = cont;

}
function openModalAddNote() {
    jQuery.noConflict();
    $('#DodawanieNotatki').modal('show');


}

function openModalLine(note, cont) {
    jQuery.noConflict();
    $('#EdycjaNotatkiLinii').modal('show');
    document.getElementById("tytulLine").value = note;
    document.getElementById("tekstLine").value = cont;

}
function openModalAddNoteLine() {
    jQuery.noConflict();
    $('#DodawanieNotatkiLinii').modal('show');


}