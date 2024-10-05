export async function drawParkour(mPx, h, w, positions, enableDrawing, drawingExercises) {
    const padding = 50;

    const height = h * mPx;
    const width = w * mPx;

    const canvas = document.querySelector("canvas");
    canvas.width = width + padding + (enableDrawing ? 200 : 0);
    canvas.height = height + padding;

    const context = canvas.getContext("2d");

    // Function to preload images
    function preloadImages(sources) {
        const images = {};
        const promises = Object.keys(sources).map((key) => {
            return new Promise((resolve, reject) => {
                const img = new Image();
                img.src = sources[key];
                img.onload = () => {
                    images[key] = img;
                    resolve();
                };
                img.onerror = () => {
                    console.error("Image failed to load:", sources[key]);
                    reject();
                };
            });
        });
        return Promise.all(promises).then(() => images);
    }

    // Extract exercise sources for preloading
    const exerciseSources = {};
    positions.forEach(position => {
        position.exercises.forEach(exercise => {
            exerciseSources[exercise.src] = exercise.src; // Use src as key for preloading
        });
    });

    const drawingSources = {};
    drawingExercises.forEach(exercise => {
        drawingSources[exercise.src] = exercise.src; // Use src as key for preloading
    });

    // Preload all images
    const allImages = await preloadImages({ ...exerciseSources, ...drawingSources });

    function drawIt() {
        context.clearRect(0, 0, canvas.width, canvas.height); // Clear the canvas

        // Draw grid
        for (let x = 0; x <= width; x += mPx) {
            context.moveTo(x + padding, padding);
            context.lineTo(x + padding, height + padding);
        }

        for (let y = 0; y <= height; y += mPx) {
            context.moveTo(padding, y + padding);
            context.lineTo(width + padding, y + padding);
        }

        context.strokeStyle = "grey";
        context.stroke();

        // Draw positions and exercises
        for (const position of positions) {
            let imgPadding = 0;
            let numberPadding = 0;

            for (const exercise of position.exercises) {
                let currentPadding = imgPadding;
                let currentNumberPadding = numberPadding;

                if (exercise.src in allImages) {
                    context.drawImage(allImages[exercise.src], position.x * mPx + padding + currentPadding + currentNumberPadding, position.y * mPx + padding, mPx, mPx);

                    // Draw exercise number over the image
                    if (exercise.number !== "") {
                        imgPadding += mPx;

                        const squareX = position.x * mPx + padding + currentPadding + currentNumberPadding;
                        const squareY = position.y * mPx + padding;

                        context.fillStyle = 'lightblue';
                        context.fillRect(squareX, squareY, mPx / 2, mPx / 2);

                        context.fillStyle = 'blue';
                        const font = 'bold ' + mPx / 2 + 'px Arial';
                        context.font = font;

                        context.textAlign = 'center';
                        context.textBaseline = 'middle';

                        const textX = squareX + mPx / 4;
                        const textY = squareY + mPx / 4;
                        context.fillText(exercise.number, textX, textY);
                    }
                }
            }
        }
    }
    drawIt();

    // Enable drawing/dragging if required
    if (enableDrawing) {
        let exercisePositions = new Array(drawingExercises.length);
        const exercisePadding = 10; // Renamed for clarity

        function drawExerciseList(initialSet) {
            for (let exerciseIdx = 0; exerciseIdx < drawingExercises.length; exerciseIdx++) {
                const exercise = drawingExercises[exerciseIdx];

                if (initialSet) {
                    exercisePositions[exerciseIdx] = {
                        x: width + mPx + exercisePadding,
                        y: exerciseIdx * mPx + exercisePadding,
                        width: mPx,
                        height: mPx
                    };
                }

                if (exercise.src in allImages) {
                    context.drawImage(allImages[exercise.src], exercisePositions[exerciseIdx].x, exercisePositions[exerciseIdx].y, exercisePositions[exerciseIdx].width, exercisePositions[exerciseIdx].height);
                }
            }
        }
        drawExerciseList(true);

        let isDragging = false;
        let dragTarget = null;
        let movingExercise = null;

        function drawMovingExercise() {
            drawIt();
            drawExerciseList(false);
        }
        drawMovingExercise();

        function isInsideAnyExercise(x, y) {
            for (const exercisePosition of exercisePositions) {
                let isInside = x >= exercisePosition.x && x <= exercisePosition.x + exercisePosition.width &&
                    y >= exercisePosition.y && y <= exercisePosition.y + exercisePosition.height;

                if (isInside) {
                    movingExercise = exercisePosition;
                    return true;
                }
            }
        }

        function getMousePos(evt) {
            const rect = canvas.getBoundingClientRect();
            return {
                x: evt.clientX - rect.left,
                y: evt.clientY - rect.top
            };
        }

        canvas.addEventListener('mousedown', (evt) => {
            const mousePos = getMousePos(evt);
            if (isInsideAnyExercise(mousePos.x, mousePos.y)) {
                isDragging = true;
                dragTarget = {
                    offsetX: mousePos.x - movingExercise.x,
                    offsetY: mousePos.y - movingExercise.y
                };
            }
        });

        canvas.addEventListener('mousemove', (evt) => {
            if (isDragging) {
                const mousePos = getMousePos(evt);
                movingExercise.x = mousePos.x - dragTarget.offsetX;
                movingExercise.y = mousePos.y - dragTarget.offsetY;
                drawMovingExercise();
            }
        });

        canvas.addEventListener('mouseup', () => {
            isDragging = false;
            dragTarget = null;
            movingExercise = null;
        });
    }
}
