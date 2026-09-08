document.querySelectorAll('.copy').forEach(button => {
    button.addEventListener('click', async () => {
        const code = button.nextElementSibling.innerText;
        await navigator.clipboard.writeText(code);
        const original = button.textContent;
        button.textContent = 'Copied';
        setTimeout(() => button.textContent = original, 300);
    });
});