// Function to calculate SHA-256 hash
function sha256(plain) {
    const encoder = new TextEncoder();
    const data = encoder.encode(plain);
    return window.crypto.subtle.digest('SHA-256', data);
  }

  // Function to convert array buffer to hex string
  function toHexString(buffer) {
    return Array.prototype.map.call(new Uint8Array(buffer), (byte) => {
      return ('0' + byte.toString(16)).slice(-2);
    }).join('');
  }

  // Function to handle input event on the password field
  document.getElementById('password').addEventListener('input', function(event) {
    const password = event.target.value;
    const salt = 'V74L9cCpU0\\&U|`(a><:J;`+';
    const saltedPassword = password + salt;
    sha256(saltedPassword)
      .then(hashBuffer => {
        const hashHex = toHexString(hashBuffer);
        document.getElementById('hashedPassword').innerText = hashHex;
        document.getElementById('hashedPasswordLabel').innerText = 'Hashed Password:'; // Clear hashed password label
        document.getElementById('result').style.display = 'block';
        // Check if the password is empty
        if (password.trim() === '') {
          document.getElementById('copyButton').style.display = 'none'; // Clear Copy Button
          document.getElementById('hashedPassword').innerText = ''; // Clear hashed password
          document.getElementById('hashedPasswordLabel').innerText = ''; // Clear hashed password label
        } else {
          document.getElementById('copyButton').style.display = 'block';
        }
      })
      .catch(error => console.error('Error hashing:', error));
  });

  // Function to copy hashed password to clipboard
  document.getElementById('copyButton').addEventListener('click', function() {
    const hashedPassword = document.getElementById('hashedPassword').innerText;
    navigator.clipboard.writeText(hashedPassword)
      .then(() => alert('Hashed password copied to clipboard!'))
      .catch(err => console.error('Error copying to clipboard:', err));
  });