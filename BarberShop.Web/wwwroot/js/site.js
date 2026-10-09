(() => {
    const dateStrip = document.querySelector('.date-strip');
    if (!dateStrip) return;
    const today = new Date();
    const state = { service: 'Corte de cabelo', barber: 'Sem preferência', price: 45, duration: 40, date: today, time: null };
    const formatDate = date => date.toLocaleDateString('pt-BR', { day: '2-digit', month: 'short' });
    const confirmation = document.querySelector('.confirmation');
    function update() {
        document.querySelector('#summary-service').textContent = state.service;
        document.querySelector('#summary-barber').textContent = state.barber;
        document.querySelector('#summary-date').textContent = formatDate(state.date);
        document.querySelector('#summary-time').textContent = state.time || 'Escolha um horário';
        document.querySelector('#summary-duration').textContent = `${state.duration} minutos`;
        document.querySelector('#summary-price').textContent = state.price.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
        document.querySelector('.confirm').disabled = !state.time;
        confirmation.hidden = true;
    }
    function select(button, selector) {
        document.querySelectorAll(selector).forEach(item => {
            item.classList.toggle('selected', item === button);
            item.setAttribute('aria-pressed', String(item === button));
        });
    }
    document.querySelector('.month-label').textContent = today.toLocaleDateString('pt-BR', { month: 'long', year: 'numeric' });
    for (let i = 0; i < 7; i++) {
        const date = new Date(today.getFullYear(), today.getMonth(), today.getDate() + i);
        const button = document.createElement('button');
        button.type = 'button';
        button.dataset.date = date.toLocaleDateString('en-CA');
        button.className = i === 0 ? 'selected' : '';
        button.setAttribute('aria-pressed', String(i === 0));
        button.setAttribute('aria-label', date.toLocaleDateString('pt-BR', { weekday: 'long', day: 'numeric', month: 'long' }));
        const label = document.createElement('small');
        label.textContent = i === 0 ? 'HOJE' : date.toLocaleDateString('pt-BR', { weekday: 'short' }).replace('.', '').toUpperCase();
        const number = document.createElement('strong');
        number.textContent = date.getDate();
        button.append(label, number);
        button.addEventListener('click', () => {
            select(button, '.date-strip button');
            state.date = date;
            state.time = null;
            document.querySelectorAll('[data-time]').forEach(item => { item.classList.remove('selected'); item.setAttribute('aria-pressed', 'false'); });
            document.querySelector('.month-label').textContent = date.toLocaleDateString('pt-BR', { month: 'long', year: 'numeric' });
            update();
        });
        dateStrip.append(button);
    }
    document.querySelectorAll('.service').forEach(button => button.addEventListener('click', () => {
        select(button, '.service');
        state.service = button.dataset.name;
        state.price = Number(button.dataset.price);
        state.duration = Number(button.dataset.duration);
        update();
    }));
    document.querySelectorAll('.barber').forEach(button => button.addEventListener('click', () => {
        select(button, '.barber');
        state.barber = button.dataset.barber;
        update();
    }));
    document.querySelectorAll('[data-time]').forEach(button => {
        button.setAttribute('aria-pressed', 'false');
        button.addEventListener('click', () => { select(button, '[data-time]'); state.time = button.dataset.time; update(); });
    });
    document.querySelector('.confirm').addEventListener('click', () => {
        const params = new URLSearchParams({ service: state.service, barber: state.barber, price: String(state.price), duration: String(state.duration), date: state.date.toISOString(), time: state.time });
        window.location.assign(`/Home/Agendamento?${params.toString()}`);
    });
    const saved = new URLSearchParams(window.location.search);
    const savedDate = new Date(saved.get('date') || '');
    if (!Number.isNaN(savedDate.getTime())) {
        const dateButton = [...dateStrip.querySelectorAll('button')].find(button => button.dataset.date === savedDate.toLocaleDateString('en-CA'));
        if (dateButton) {
            dateButton.click();
            const serviceButton = [...document.querySelectorAll('.service')].find(button => button.dataset.name === saved.get('service'));
            const barberButton = [...document.querySelectorAll('.barber')].find(button => button.dataset.barber === saved.get('barber'));
            const timeButton = [...document.querySelectorAll('[data-time]')].find(button => button.dataset.time === saved.get('time'));
            serviceButton?.click();
            barberButton?.click();
            timeButton?.click();
        }
    }
    update();
})();

(() => {
    const params = new URLSearchParams(window.location.search);
    const services = { 'Corte de cabelo': { price: 45, duration: 40 }, 'Barba completa': { price: 35, duration: 30 }, 'Corte + barba': { price: 70, duration: 60 } };
    const service = params.get('service');
    const barber = params.get('barber');
    const time = params.get('time');
    const date = new Date(params.get('date') || '');
    const valid = Object.hasOwn(services, service) && ['Sem preferência', 'Rafael', 'Lucas'].includes(barber) && ['09:00', '09:40', '11:00', '11:40', '13:00', '13:40', '14:20', '15:40', '16:20', '17:00'].includes(time) && !Number.isNaN(date.getTime());
    const query = valid ? `?${params.toString()}` : '';
    const login = document.querySelector('#login-form');
    if (login) {
        const guestLink = document.querySelector('#guest-link');
        if (guestLink) {
            guestLink.href = valid ? `/Home/Agendamento${query}` : '/#agenda';
            guestLink.firstChild.textContent = valid ? 'Continuar sem uma conta ' : 'Escolher um horário ';
        }
        const feedback = document.querySelector('#login-feedback');
        document.querySelector('#toggle-password').addEventListener('click', event => {
            const input = document.querySelector('#login-password');
            const visible = input.type === 'password';
            input.type = visible ? 'text' : 'password';
            event.currentTarget.textContent = visible ? 'Ocultar' : 'Mostrar';
            event.currentTarget.setAttribute('aria-label', visible ? 'Ocultar senha' : 'Mostrar senha');
            event.currentTarget.setAttribute('aria-pressed', String(visible));
        });
        document.querySelector('#forgot-password').addEventListener('click', () => {
            feedback.textContent = 'A recuperação de senha estará disponível quando o login estiver conectado. Nesta prévia, você pode seguir sem uma conta.';
            feedback.hidden = false;
        });
        login.addEventListener('submit', event => {
            event.preventDefault();
            login.reset();
            document.querySelector('#login-password').type = 'password';
            const toggle = document.querySelector('#toggle-password');
            toggle.textContent = 'Mostrar';
            toggle.setAttribute('aria-label', 'Mostrar senha');
            toggle.setAttribute('aria-pressed', 'false');
            if (document.querySelector('#account-panel') && valid) {
                document.dispatchEvent(new Event('demo-login-complete'));
            } else if (valid) window.location.assign(`/Home/Agendamento${query}`);
            else {
                feedback.textContent = 'Prévia de login concluída. Nenhuma autenticação foi realizada. Escolha seu corte para explorar o agendamento.';
                feedback.hidden = false;
            }
        });
    }
    const booking = document.querySelector('#booking-form');
    if (!booking) return;
    if (!valid) {
        window.location.replace('/#agenda');
        return;
    }
    document.querySelector('#checkout-content').hidden = false;
    document.querySelectorAll('a[href="/#agenda"]').forEach(link => { link.href = `/${query}#agenda`; });
    document.querySelectorAll('[data-identity]').forEach(button => button.addEventListener('click', () => {
        const account = button.dataset.identity === 'account';
        document.querySelector('#account-panel').hidden = !account;
        document.querySelector('#guest-panel').hidden = account;
        document.querySelectorAll('[data-identity]').forEach(item => {
            const selected = item === button;
            item.classList.toggle('selected', selected);
            item.setAttribute('aria-pressed', String(selected));
        });
    }));
    const formattedDate = date.toLocaleDateString('pt-BR', { day: '2-digit', month: 'long' });
    const details = services[service];
    document.querySelector('#review-service').textContent = service;
    document.querySelector('#review-barber').textContent = barber;
    document.querySelector('#review-date').textContent = formattedDate;
    document.querySelector('#review-time').textContent = time;
    document.querySelector('#review-duration').textContent = `${details.duration} minutos`;
    document.querySelector('#review-price').textContent = details.price.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
    function finishPreview(name) {
        document.querySelector('#success-details').textContent = `${name ? name + ', sua' : 'Sua'} escolha foi ${service.toLowerCase()}, em ${formattedDate}, às ${time}.`;
        document.querySelector('#checkout-content').hidden = true;
        document.querySelector('#checkout-heading').hidden = true;
        const success = document.querySelector('#booking-success');
        success.hidden = false;
        const step = document.querySelectorAll('.flow-progress > span')[1];
        step.className = 'completed';
        step.removeAttribute('aria-current');
        step.querySelector('b').textContent = '✓';
        booking.reset();
        success.focus();
    }
    document.addEventListener('demo-login-complete', () => finishPreview(''));
    booking.addEventListener('submit', event => {
        event.preventDefault();
        const name = document.querySelector('#customer-name').value.trim();
        if (name.length < 3) {
            document.querySelector('#customer-name').setCustomValidity('Informe seu nome com pelo menos três caracteres.');
            booking.reportValidity();
            return;
        }
        finishPreview(name);
    });
    document.querySelector('#customer-name').addEventListener('input', event => event.target.setCustomValidity(''));
})();

(() => {
    const toggle = document.querySelector('.animation-toggle');
    if (!toggle) return;
    toggle.addEventListener('click', () => {
        const paused = document.querySelector('.transformation').classList.toggle('is-paused');
        toggle.textContent = paused ? '▷' : 'Ⅱ';
        toggle.setAttribute('aria-label', paused ? 'Retomar animação' : 'Pausar animação');
        toggle.setAttribute('aria-pressed', String(paused));
    });
})();
