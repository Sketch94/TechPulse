document.querySelectorAll('a').forEach(anchor => {
    anchor.addEventListener('click', function (e) {
        const target = this.getAttribute('href');

        if (!target ||
            target.startsWith('/CreateAccount') ||
            target.startsWith('/Login') ||
            target.startsWith('/ForgottenPassword') ||
            target.startsWith('https') ||
            target.startsWith('.pdf') 
        ) { 
            return; // Om det inte finns något mål eller om det är en inre länk, gör inget
        }

        e.preventDefault(); // Förhindra standardbeteende (länkarna laddas inte om omedelbart)

        fetch(target)
            .then(response => response.text())
            .then(html => {
                const parsedHTML = new DOMParser().parseFromString(html, 'text/html');

                // Uppdaterar bara #content)
                const content = parsedHTML.querySelector('main').innerHTML;
                document.querySelector('#content').innerHTML = content;

                // laddar inte om sidam
                window.history.pushState({}, '', target);

                // Uppdatera sidtiteln baserat på innehållet på den nya sidan
                const newTitle = parsedHTML.querySelector('title').innerText;
                document.title = newTitle;

                // Initiera event listeners och animationer igen
            })
            .catch(err => console.error('Error fetching page:', err));
    });
});

function openOtpModal() {
    let username = document.getElementById("Username").value;
    if (!username) {
        alert("Ange ditt användarnamn eller mobilnummer först.")
        return;
    }

    var loginModalEl = document.getElementById("loginModal");

    var loginModalInstance = bootstrap.Modal.getInstance(loginModalEl) || new bootstrap.Modal(loginModalEl);
    loginModalInstance.hide();

    var otpModalEl = document.getElementById("otpModal");

    var otpModalInstance = new bootstrap.Modal(otpModalEl);
    otpModalInstance.show();
}

document.addEventListener('DOMContentLoaded', function () {

    const username = document.getElementById("Username");
    const password = document.getElementById("Password");
    const rememberMe = document.getElementById("rememberMe");

    const usernameError = document.getElementById("usernameError");
    const passwordError = document.getElementById("passwordError");


    function toggleCheckbox() {

        let isFilled = username.value.trim() !== "" && password.value.trim() !== "";

        rememberMe.disabled = !isFilled;

        if (!isFilled) {
            rememberMe.checked = false;
        }
    }

    username.addEventListener("input", toggleCheckbox);
    password.addEventListener("input", toggleCheckbox);

    username.addEventListener("input", function () {
        if (username.value.trim() === "") {
            username.classList.add("border-danger");
            usernameError.style.display = "block";
        } else {
            username.classList.remove("border-danger");
            usernameError.style.display = "none";
        }
    });

    password.addEventListener("input", function () {
        if (password.value.trim() === "") {
            password.classList.add("border-danger");
            passwordError.style.display = "block";
        } else {
            password.classList.remove("border-danger");
            passwordError.style.display = "none";
        }
    });
});

document.addEventListener('DOMContentLoaded', function () {
    const otpContainer = document.getElementById("otp-container");
    const verifyBtn = document.getElementById("verifyCodeBtn");
    const notFinished = document.getElementById("codeNotFinishedError")

    let otpInputs = [];

    for (let i = 0; i < 6; i++) {
        let input = document.createElement("input");
        input.type = "text";
        input.maxLength = 1;
        input.className = "otp-input form-control text-center";
        input.style.width = '40px';
        input.style.fontSize = "1.5em";
        otpContainer.appendChild(input);
        otpInputs.push(input);
        input.setAttribute("inputmode", "numeric");
        input.setAttribute("pattern", "[0-9]*")
    }

    otpInputs.forEach((input, index) => {
        input.addEventListener("input", function (e) {
            if (this.value.length === 1 && index < otpInputs.length - 1) {
                otpInputs[index + 1].focus();
            }

            checkOTPcompletion();
        });

        input.addEventListener("keydown", function (e) {
            if (e.key === "Backspace" && index > 0 && this.value.length === 0) {
                otpInputs[index - 1].focus();
            }
        });

        input.addEventListener("keypress", function (e) {
            if (!/[0-9]/.test(e.key)) {
                e.preventDefault();
            }

        })
    });

    function checkOTPcompletion() {
        let otpCode = otpInputs.map(input => input.value).join("");
        verifyBtn.disabled = otpCode.length !== 6;


        let allEmpty = otpInputs.every(input => input.value.trim() === "");
        if (allEmpty) {
            otpInputs.forEach(input => input.classList.add("border-danger"));
            notFinished.style.display = "block";
        }
        else {
            otpInputs.forEach(input => input.classList.remove("border-danger"));
            notFinished.style.display = "none";
        }
    }
});

function verifyBtnEnabled() {
    alert("Koden skickades till ditt telefonnummer!");
    document.getElementById("verifyBtn").disabled = false;
}
