// Espera a que la página esté lista
document.addEventListener('DOMContentLoaded', (event) => {
    // Busca un div con el id 'particles-js' para poner el lienzo
    // (lo añadiremos al _Layout.cshtml)
    tsParticles.load("particles-js", {
        background: {
            color: {
                value: "#0a192f" // Debe ser el mismo que tu fondo
            }
        },
        fpsLimit: 60,
        interactivity: {
            events: {
                onHover: {
                    enable: true,
                    mode: "repulse" // Las partículas se alejan del mouse
                },
                resize: true
            },
            modes: {
                repulse: {
                    distance: 100,
                    duration: 0.4
                }
            }
        },
        particles: {
            color: {
                value: "#64ffda" // Color cian de Tron para las partículas
            },
            links: {
                color: "#112240", // Color azul oscuro para las líneas
                distance: 150,
                enable: true,
                opacity: 0.5,
                width: 1
            },
            collisions: {
                enable: true
            },
            move: {
                direction: "none",
                enable: true,
                outModes: {
                    default: "bounce"
                },
                random: false,
                speed: 1, // Velocidad de movimiento lenta
                straight: false
            },
            number: {
                density: {
                    enable: true,
                    area: 800
                },
                value: 80 // Cantidad de partículas
            },
            opacity: {
                value: 0.5
            },
            shape: {
                type: "circle"
            },
            size: {
                value: { min: 1, max: 3 } // Tamaño pequeño y variado
            }
        },
        detectRetina: true
    });
});