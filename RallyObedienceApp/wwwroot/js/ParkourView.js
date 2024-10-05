export function generateUUID() {
    let
        d = new Date().getTime(),
        d2 = ((typeof performance !== 'undefined') && performance.now && (performance.now() * 1000)) || 0;
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, c => {
        let r = Math.random() * 16;
        if (d > 0) {
            r = (d + r) % 16 | 0;
            d = Math.floor(d / 16);
        } else {
            r = (d2 + r) % 16 | 0;
            d2 = Math.floor(d2 / 16);
        }
        return (c == 'x' ? r : (r & 0x7 | 0x8)).toString(16);
    });
};

export function addParkourExercise(id, exerciseId, number, left, top) {
    DotNet.invokeMethodAsync('RallyObedienceApp', 'AddParkourExerciseAsync', id, exerciseId, number, left, top)
        .then(data => {
            console.log(data);
        });
}

export function updateParkourExercise(id, exerciseId, number, left, top) {
    DotNet.invokeMethodAsync('RallyObedienceApp', 'UpdateParkourExerciseAsync', id, exerciseId, number, left, top)
        .then(data => {
            console.log(data);
        });
}

export async function drawParkour(mPx, h, w, parkour, enableDrawing, exercises) {
    const padding = 50;

    const positions = parkour.positions;

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
    exercises.forEach(exercise => {
        exerciseSources[exercise.id] = exercise.image;
    });

    // Preload all images
    const allImages = await preloadImages({ ...exerciseSources });

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

                //context.fillText(JSON.stringify(exercise), 10, 10, 1000, 1000);
                // return;
                if (exercise.exerciseId in allImages) {
                    context.drawImage(allImages[exercise.exerciseId], position.left * mPx + padding + currentPadding + currentNumberPadding, position.top * mPx + padding, mPx, mPx);

                    // Draw exercise number over the image
                    if (exercise.number !== "") {
                        imgPadding += mPx;

                        const squareX = position.left * mPx + padding + currentPadding + currentNumberPadding;
                        const squareY = position.top * mPx + padding;

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
        let exercisePositions = new Array(exercises.length);
        const exercisePadding = 10; // Renamed for clarity

        function drawExerciseList(initialSet) {
            for (let exerciseIdx = 0; exerciseIdx < exercises.length; exerciseIdx++) {
                const exercise = exercises[exerciseIdx];

                if (initialSet) {
                    exercisePositions[exerciseIdx] = {
                        id: exercise.id,
                        number: exercise.number,
                        x: width + mPx + exercisePadding,
                        y: exerciseIdx * mPx + exercisePadding,
                        width: mPx,
                        height: mPx
                    };
                }

                if (exercise.id in allImages) {
                    context.drawImage(allImages[exercise.id], exercisePositions[exerciseIdx].x, exercisePositions[exerciseIdx].y, exercisePositions[exerciseIdx].width, exercisePositions[exerciseIdx].height);
                }
            }
        }
        drawExerciseList(true);

        let isDragging = false;
        let dragTarget = null;
        let movingExercise = null;
        let adding = false;
        let updating = false;
        let deleting = false;

        function drawMovingExercise() {
            if (movingExercise != null) {
                var position = positions.find(p => p.id == movingExercise.id_position);
                if (position != null) {
                    position.left = (movingExercise.x - padding) / mPx;
                    position.top = (movingExercise.y - padding) / mPx;
                }
            }

            drawIt();
            drawExerciseList(false);

            if (movingExercise !== null)
                context.fillText("Moving exercise: " + movingExercise.x + ', ' + movingExercise.y, 10, 10);
            
        }
        drawMovingExercise();

        function isInsideAnyExercise(x, y) {
            for (const exercisePosition of exercisePositions) {
                let isInside = x >= exercisePosition.x && x <= exercisePosition.x + exercisePosition.width &&
                    y >= exercisePosition.y && y <= exercisePosition.y + exercisePosition.height;

                if (isInside) {
                    movingExercise = exercisePosition;
                    updating = false;
                    deleting = false;
                    adding = true;
                    return true;
                }
            }

            if (adding == false && updating == false && deleting == false) {
                for (const position of positions) {
                    for (const exercise of position.exercises) {
                        let isInside = x >= position.left * mPx + padding && x <= position.left * mPx + padding + mPx &&
                            y >= position.top * mPx + padding && y <= position.top * mPx + padding + mPx;

                        if (isInside) {
                            movingExercise = {
                                id: exercise.exerciseId,
                                id_position: position.id,
                                x: position.left * mPx + padding,
                                y: position.top * mPx + padding
                            };
                            updating = true;
                            deleting = true;
                            adding = false;
                            return true;
                        }
                    }
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

            // TODO: second parameter is number
            if (adding) {
                let uuid = generateUUID();
                addParkourExercise(uuid, movingExercise.id, "", (movingExercise.x - padding) / mPx, (movingExercise.y - padding) / mPx);

                positions.push({
                    id: uuid,
                    exercises: [{
                        exerciseId: movingExercise.id,
                        number: ""
                    }],
                    left: (movingExercise.x - padding) / mPx,
                    top: (movingExercise.y - padding) / mPx
                });

                adding = false;
            }

            if (updating) {
                let exercisePadding = 0;
                for (let exercise of positions.find(p => p.id == movingExercise.id_position).exercises) {
                    updateParkourExercise(movingExercise.id_position, exercise.id, exercise.number, (movingExercise.x - padding + exercisePadding) / mPx, (movingExercise.y - padding) / mPx);

                    exercisePadding += mPx;
                };
                
                updating = false;
            }

            movingExercise = null;

            drawExerciseList(true);
        });
    }
}
