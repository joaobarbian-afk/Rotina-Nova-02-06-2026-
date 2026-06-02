const host =
    "wss://c9e410c37105493594c09032bcfd99b0.s1.eu.hivemq.cloud:8884/mqtt";

const options = {
    username: "Admin",
    password: "Senai2026@"
};

let client = null;

const labels = [];

const temperatureData = [];
const humidityData = [];
const proximityData = [];

const temperatureChart = new Chart(
    document.getElementById("temperatureChart"),
    {
        type: "line",
        data: {
            labels: labels,
            datasets: [{
                label: "Temperatura °C",
                data: temperatureData
            }]
        }
    }
);

const humidityChart = new Chart(
    document.getElementById("humidityChart"),
    {
        type: "line",
        data: {
            labels: labels,
            datasets: [{
                label: "Umidade %",
                data: humidityData
            }]
        }
    }
);

const proximityChart = new Chart(
    document.getElementById("proximityChart"),
    {
        type: "line",
        data: {
            labels: labels,
            datasets: [{
                label: "Proximidade cm",
                data: proximityData
            }]
        }
    }
);

function addData(chart, array, value) {
    if (array.length >= 20) {
        array.shift();
        chart.data.labels.shift();
    }

    const now = new Date().toLocaleTimeString();

    chart.data.labels.push(now);
    array.push(value);

    chart.update();
}

document
    .getElementById("btnStart")
    .addEventListener("click", () => {

        if (client && client.connected) {
            return;
        }

        client = mqtt.connect(host, options);

        client.on("connect", () => {

            console.log("MQTT conectado");

            client.subscribe("sensores/temperatura");
            client.subscribe("sensores/umidade");
            client.subscribe("sensores/proximidade");

            // INICIA A SIMULAÇÃO
            client.publish(
                "controle/start",
                "START"
            );

            console.log("Simulação iniciada");
        });

        client.on("message", (topic, message) => {

            const value =
                Number(message.toString());

            if (topic === "sensores/temperatura") {

                addData(
                    temperatureChart,
                    temperatureData,
                    value
                );
            }

            if (topic === "sensores/umidade") {

                addData(
                    humidityChart,
                    humidityData,
                    value
                );
            }

            if (topic === "sensores/proximidade") {

                addData(
                    proximityChart,
                    proximityData,
                    value
                );
            }
        });
    });

document
    .getElementById("btnStop")
    .addEventListener("click", () => {

        if (!client) {
            return;
        }

        client.publish(
            "controle/stop",
            "STOP"
        );

        console.log("Simulação parada");

        client.end();

        client = null;
    });