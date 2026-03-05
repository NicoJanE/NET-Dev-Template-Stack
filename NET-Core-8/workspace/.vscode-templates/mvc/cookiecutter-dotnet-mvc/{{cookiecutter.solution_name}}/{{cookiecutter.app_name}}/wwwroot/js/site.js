// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code
// Prefered code-style: Allman/BSD style (No  formatting available, everyone is happy with K&R style)

// Global WASM instance
let wasmInstance;
let wasmLoader;

// BEGIN: nje-table
// =====================================================================
//      - Row Selectors
//      - Action button enable/disable based on selection
//
const tableBody = document.querySelector(".nje-table-body-scroll .nje-table");
const actionButtons = document.querySelectorAll(".nje-table-action-btn");
let selectedRow = null;

// Initialize buttons as disabled on page load
actionButtons.forEach((btn) => 
{
  btn.disabled = true;
});

if (tableBody)
{
  tableBody.addEventListener("click", (event) => 
  {
    const rows = tableBody.querySelectorAll("tbody tr");
    // Remove the selected class from all rows
    rows.forEach((row) => 
    {
      row.classList.remove("selected");
    });
    // Add the selected class to the clicked row
    const row = event.target.closest("tr");
    if (row && row.closest("tbody")) 
    {
      row.classList.add("selected");
      selectedRow = row;
      // Enable action buttons
      actionButtons.forEach((btn) => 
      {
        btn.disabled = false;
      });
    }
    else
    {
      selectedRow = null;
      // Disable action buttons
      actionButtons.forEach((btn) => 
      {
        btn.disabled = true;
      });
    }
  });
}

// Action button handlers
actionButtons.forEach((btn) => 
{
  btn.addEventListener("click", (event) => 
  {
    event.stopPropagation();
    if (!btn.disabled && selectedRow)
    {
      const action = btn.className.replace("nje-table-action-btn ", "").trim();
      console.log(`Action: ${action} on row:`, selectedRow);
      // Handle action: add, edit, delete
    }
  });
});

// END: nje-table
// =====================================================================


/* BEGIN Collapse
    - For CSS class: nje-contentbox-flex-child 
 =====================================================================*/
 document.addEventListener('DOMContentLoaded', RegisterCollapsibleButtons);  
 document.addEventListener('DOMContentLoaded', InitSidePanel );
 window.addEventListener('load', initWasm);


/*
      PURPOSE:  Registers click events for all buttons with the 'collapsible' class.                                                */
function RegisterCollapsibleButtons() 
{
    const triggerButton = document.querySelectorAll('.collapsible'); 
    if (triggerButton)
    {
        triggerButton.forEach(button => 
        {
            const targetId = button.getAttribute('data-target');
            button.addEventListener("click", (event) =>
            {
              event.stopPropagation();
              console.log("clicked - toggling collapse")
              Toggle(targetId, button);          
            });
          });
    }
}

/*
      PURPOSE: Toggles the 'collapsed' class on a target element and updates button symbol.
          @param {string} elementId - The ID of the element to toggle.   
          @param {HTMLElement} button - The button that triggered the toggle.                                                              */
function Toggle(elementId, button) {
  const targetBlock = document.getElementById(elementId);
  if (targetBlock) {
    targetBlock.classList.toggle('collapsed');
    
    // Also toggle class on parent container for ones that have collapsible content
    const parentContainer = targetBlock.closest('.nje-contentbox-flex-child');
    if (parentContainer) {
      if (targetBlock.classList.contains('collapsed')) {
        parentContainer.classList.add('has-collapsed-content');
      } else {
        parentContainer.classList.remove('has-collapsed-content');
      }
    }
    
    // Update button symbol based on collapsed state
    // ▼ = down arrow (expanded), ▲ = up arrow (collapsed)
    if (targetBlock.classList.contains('collapsed')) {
      button.textContent = '▲';
    } else {
      button.textContent = '▼';
    }
    
    console.log(`Toggled collapse for element: ${elementId}`);
  } else {
    console.error(`Element with ID "${elementId}" not found.`);
  }
}



/* END Collapse
 =====================================================================*/



/* BEGIN Side Panel Toggle
   Separate from the generic collapsible system
   =====================================================================*/
function InitSidePanel() 
{
    //const panel = document.getElementById('panel');
    const panel = document.querySelector('.panel');
    const btn = panel ? panel.querySelector('.panel-toggle') : null;
    const arrow = btn ? btn.querySelector('.panel-arrow') : null;

    if (!panel || !btn || !arrow) {
        console.warn('Side panel elements not found');
        return;
    }

    btn.addEventListener('click', function (event) 
    {
        event.stopPropagation();
        const collapsed = panel.classList.toggle('collapsed');
        
        if (collapsed) 
        {
            arrow.innerHTML = '&#8594;'; // Right arrow
            btn.setAttribute('aria-label', 'Expand panel');
        } 
        else 
        {
            arrow.innerHTML = '&#8592;'; // Left arrow
            btn.setAttribute('aria-label', 'Collapse panel');
        }
    });
}
/* END Side Panel Toggle
   =====================================================================*/

  



    // Initialize WASM module using AssemblyScript loader
  async function initWasm() {
    try {
      // Fetch and import the compiled WASM module loader
      const wasmModule = await import('/js/build/release.js');
      wasmLoader = wasmModule;
      wasmInstance = wasmModule;
      
      // Only update status element if it exists on the page
      const statusElement = document.getElementById('status');
      if (statusElement) {
        statusElement.textContent = '✓ WASM module loaded successfully!';
        statusElement.className = 'status found';
      }
      console.log('✓ WASM module loaded successfully!');
    } catch (error) {
      const statusElement = document.getElementById('status');
      if (statusElement) {
        statusElement.textContent = '✗ Error loading WASM: ' + error.message;
        statusElement.className = 'status not-found';
      }
      console.error('WASM Error:', error);
    }
  }

  // Check if target CSS class exists in page DOM via WASM
  function checkTargetClass() {
    if (!wasmLoader || !wasmLoader.searchForClass) {
      alert('WASM not loaded yet!');
      return;
    }

    try {
      // Collect all CSS classes from page
      const allElements = document.querySelectorAll('[class]');
      const allClasses = new Set();
      
      allElements.forEach(el => {
        el.className.split(/\s+/).forEach(cls => {
          if (cls) allClasses.add(cls);
        });
      });
      
      const classString = Array.from(allClasses).join(',');
      
      // Call WASM function directly
      const result = wasmLoader.searchForClass(classString);
      
      console.log('WASM searchForClass result:', result);
      alert('Target class: ' + result + '\n\nTotal classes on page: ' + allClasses.size);
    } catch (error) {
      console.error('Error:', error);
      alert('Error: ' + error.message);
    }
  }

  // Check if target CSS class exists in site.css file via WASM
  async function checkTargetClassInCSS() {
    if (!wasmLoader || !wasmLoader.searchForClass) {
      alert('WASM not loaded yet!');
      return;
    }

    try {
      // Fetch the site.css file
      const response = await fetch('/css/site.css');
      if (!response.ok) {
        throw new Error(`Failed to fetch CSS: ${response.status}`);
      }
      
      const cssContent = await response.text();
      
      // Pass CSS content to WASM function
      const result = wasmLoader.searchForClass(cssContent);
      
      console.log('WASM searchForClass in CSS result:', result);
      alert('Target class in CSS: ' + result + '\n\nCSS file size: ' + cssContent.length + ' bytes');
    } catch (error) {
      console.error('Error:', error);
      alert('Error: ' + error.message);
    }
  }

