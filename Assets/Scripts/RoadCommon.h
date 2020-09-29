#ifndef ROAD_COMMON_H
#define ROAD_COMMON_H

inline float PERSPECTIVE_FACTOR() {
    return 1.05f;
}

inline float PERSPECTIVE(float yPos, float height) {
    return yPos / (height * PERSPECTIVE_FACTOR());
}

inline float CURVE(float curvature, float perspective)
{
    return curvature * pow(perspective, 3.0f);
}

inline float MOVE_ILLUSION_FUNCTION(float factor, float perspective, float distance) {
    return sin(factor * pow(perspective, 3.0f) + distance * 0.1f);
}

#endif