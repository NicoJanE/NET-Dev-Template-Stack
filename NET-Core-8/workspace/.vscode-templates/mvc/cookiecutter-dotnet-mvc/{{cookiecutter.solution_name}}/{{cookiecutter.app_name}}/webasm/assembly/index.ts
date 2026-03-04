// AssemblyScript WASM Module for CSS Class Validation
// Purpose: Search for the target CSS class in a comma-separated string of classes

// Build: 
// - npm install -D assemblyscript
// - npm run asbuild

const TARGET_CLASS: string = "nje-DCFBCD07-8B5C-4E6B-AFBC-59668C63DB75";

/**
 * Simple substring search for the target class
 * @param classNames - Comma-separated string of CSS classes
 * @returns true if target class is found, false otherwise
 */
export function searchForClass(classNames: string): string {
  if (classNames.includes(TARGET_CLASS)) {
    return "Valid!";
  }
  return "NOT_FOUND";
}

