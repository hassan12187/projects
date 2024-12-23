// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

const navLinks = document.querySelectorAll("#navbar ul li a");
const windowLocation = window.location.href.toLowerCase();
console.log(windowLocation);
navLinks.forEach((Links) => {
    if (Links.href.toLowerCase() === windowLocation) {
        Links.classList.add("active");
    };
});
// Write your JavaScript code.
let alert = document.querySelector(".alert");
if (alert.textContent.trim() !== "") {
        alert.classList.remove("d-none");
    setTimeout(() => {
        $(".alert").alert('close')
    }, 5000)
};
