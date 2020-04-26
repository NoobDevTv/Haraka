closeNav();

function toggleMenu() {
  let isActive = document.getElementById("hamburg").checked;

  if (isActive) {
    closeNav();
  } else {
    openNav();
  }
}

/* Set the width of the sidebar to 250px and the left margin of the page content to 250px */
function openNav() {
  document.getElementById("mySidebar").style.width = "250px";
  document.getElementById("main-content").style.marginLeft = "250px";
}

/* Set the width of the sidebar to 0 and the left margin of the page content to 0 */
function closeNav() {
  document.getElementById("mySidebar").style.width = "50px";
  document.getElementById("main-content").style.marginLeft = "50px";
}
