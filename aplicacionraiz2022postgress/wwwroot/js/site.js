// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const Usuario= document.querySelector('.Usuario');
const Descripcion = document.querySelector('.Descripcion');

Usuario.addEventListener('mouseover',()=>{
    Descripcion.classList.add("active");
})
Usuario.addEventListener('mouseout',()=>{
    Descripcion.classList.remove("active");
})