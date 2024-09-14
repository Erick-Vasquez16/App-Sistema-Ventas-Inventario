function mostrarAlertaGuardar() {
    Swal.fire({
        position: "center",
        icon: "success",
        title: "¡Tu producto ha sido editado!",
        showConfirmButton: false,
        timer: 1500
    }).then(() => {
        // Enviar el formulario después de la alerta
        document.getElementById("editarProductoForm").submit(); // Envía el formulario manualmente
    });
}

function confirmarVolver(event) {
    event.preventDefault(); // Prevenir la redirección inmediata

    // Mostrar la alerta de confirmación
    Swal.fire({
        title: "¿Está seguro?",
        text: "¡Se perderán los datos ingresados!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Sí, regresar",
        cancelButtonText: "Cancelar"
    }).then((result) => {
        if (result.isConfirmed) {
            // Si el usuario confirma, redirigir a la página de lista de productos
            window.location.href = '@Url.Action("Lista", "Producto")';
        }
    });
}