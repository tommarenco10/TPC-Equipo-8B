    function previewImage(event) {
        const fileInput = event.target;
    const file = fileInput.files[0];

    if (file) {
            const reader = new FileReader();

    reader.onload = function(e) {
                const imgPreview = document.getElementById('imgPreview');
    imgPreview.src = e.target.result; // Cargar la imagen en el atributo src
    imgPreview.style.display = 'block'; // Mostrar la imagen
            };

    reader.readAsDataURL(file); // Leer el archivo como Data URL
        }
    }
