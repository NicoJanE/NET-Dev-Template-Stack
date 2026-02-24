/*    'Help JavaScript' 
        Works on click item with class with selector: 'nje_app-help-icon'. Insert model data transfered with attribut: 'data-model'   */    
document.addEventListener('DOMContentLoaded', function() 
{
    const helpIcons = document.querySelectorAll('.nje_app-help-icon');

    helpIcons.forEach(icon => {
        icon.addEventListener('click', function(e) {
            document.getElementById('view-HelpText').textContent = this.getAttribute('data-model');
        });
    });
});
    