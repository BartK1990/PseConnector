// Thin Chart.js wrapper used by the PseLineChart component, shared by every
// PSE connector page. Chart.js itself is loaded globally (see index.html);
// this module only adapts the component's plain data shape into a Chart.js
// config and keeps one chart instance alive per canvas for in-place updates.

function buildScales(yTitle, y1Title) {
    const scales = {
        x: {},
        y: {
            type: 'linear',
            position: 'left',
            title: { display: !!yTitle, text: yTitle ?? '' },
        },
    };

    if (y1Title) {
        scales.y1 = {
            type: 'linear',
            position: 'right',
            title: { display: true, text: y1Title },
            grid: { drawOnChartArea: false },
        };
    }

    return scales;
}

// Draws a vertical line through the hovered x-position across the full
// plot height, matching the crosshair Blazor.Bootstrap's chart used to
// provide out of the box. Chart.js has no built-in equivalent, so it's
// implemented as a small inline plugin scoped to this chart instance.
const verticalHoverLinePlugin = {
    id: 'verticalHoverLine',
    afterDraw(chart) {
        const active = chart.tooltip?._active;
        if (!active || !active.length) {
            return;
        }

        const { ctx, chartArea } = chart;
        const x = active[0].element.x;

        ctx.save();
        ctx.beginPath();
        ctx.moveTo(x, chartArea.top);
        ctx.lineTo(x, chartArea.bottom);
        ctx.lineWidth = 1;
        ctx.strokeStyle = 'rgba(100, 100, 100, 0.6)';
        ctx.setLineDash([4, 4]);
        ctx.stroke();
        ctx.restore();
    },
};

function mapData(data) {
    return {
        labels: data.labels,
        datasets: (data.series ?? []).map(s => ({
            label: s.label,
            data: s.data,
            borderColor: s.color,
            backgroundColor: s.color,
            yAxisID: s.axisId ?? 'y',
            borderDash: s.borderDash ?? undefined,
            borderWidth: s.borderWidth ?? 2,
            pointBackgroundColor: s.pointColors ?? s.color,
            pointBorderColor: s.pointColors ?? s.color,
            pointRadius: s.pointRadii ?? (s.pointRadius ?? 1),
            pointHoverRadius: s.pointHoverRadii ?? (s.pointHoverRadius ?? 3),
            tension: 0,
        })),
    };
}

export function create(canvasElement, initialData, yTitle, y1Title) {
    const chart = new Chart(canvasElement.getContext('2d'), {
        type: 'line',
        data: mapData(initialData),
        options: {
            responsive: true,
            maintainAspectRatio: false,
            interaction: { mode: 'index', intersect: false },
            scales: buildScales(yTitle, y1Title),
        },
        plugins: [verticalHoverLinePlugin],
    });

    return {
        update(newData) {
            chart.data = mapData(newData);
            chart.update();
        },
        destroy() {
            chart.destroy();
        },
    };
}
