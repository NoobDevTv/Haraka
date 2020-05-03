function toggleMenu() {
  let checked = document.getElementById("hamburg").checked;

  if (checked) {
    openNav();    
  } else {
    closeNav();
  }
}

/* Set the width of the sidebar to 250px and the left margin of the page content to 250px */
function openNav() {
  document.getElementById("mySidebar").classList.remove("not-active");
  document.getElementById("main-content").classList.remove("full");
}

/* Set the width of the sidebar to 0 and the left margin of the page content to 0 */
function closeNav() {
  document.getElementById("mySidebar").classList.add("not-active");
  document.getElementById("main-content").classList.add("full");
}
