﻿
window.onload = function () {
    const imgPreview = document.getElementById("imgJugador");

    // Si ya tiene un src, quito la clase d-none para que se muestre
    if (imgPreview.src && imgPreview.src !== "") {
        imgPreview.classList.remove("d-none");
    }
};




function validarTamanoYVistaPrevia(input) {
    const archivo = input.files[0];
    const imgPreview = document.getElementById("imgPreview");

    if (archivo) {
        // Validación opcional del tamaño o tipo
        if (archivo.size > 2 * 1024 * 1024) { // 2 MB
            alert("El archivo es demasiado grande.");
            return;
        }

        // Mostrar la vista previa
        imgPreview.src = URL.createObjectURL(archivo);
        imgPreview.classList.remove("d-none");
    }
}


function validarTamanoYVistaPreviaJugador(input) {
    const archivo = input.files[0];
    const imgPreview = document.getElementById("imgJugador");

    if (archivo) {
        // Validación opcional del tamaño o tipo
        if (archivo.size > 2 * 1024 * 1024) { // 2 MB
            alert("El archivo es demasiado grande.");
            return;
        }

        // Mostrar la vista previa
        imgPreview.src = URL.createObjectURL(archivo);
        imgPreview.classList.remove("d-none");
    }
}