// Given a 3d position as a seed, compute a smooth procedural noise
// value: "Perlin Noise", also known as "Gradient noise".
//
// Inputs:
//   st  3D seed
// Returns a smooth value between (-1,1)
//
// expects: random_direction, smooth_step
float perlin_noise( vec3 st) 
{
  /////////////////////////////////////////////////////////////////////////////
  vec3 left_down = floor(st);

  vec3 corners[8];
  int idx = 0;
  for (int i = 0; i < 2; i++){
    for (int j = 0; j < 2; j++){
      for (int k = 0; k < 2; k++){
        corners[idx++] = left_down + vec3(i, j, k);
      }
    }
  }
  vec3 related_posi = st - left_down;

  vec3 gradients[8];
  for(int i = 0; i < 8; i++){
    gradients[i] = random_direction(corners[i]);
  }

  float dot_products[8];

  for (int i = 0; i < 8; i++){
    dot_products[i] = dot(st - corners[i], gradients[i]);
  }

  vec3 smoothed_posi = smooth_step(related_posi);

  float interpolate1 = mix(dot_products[0], dot_products[4], smoothed_posi.x);
  float interpolate2 = mix(dot_products[1], dot_products[5], smoothed_posi.x);
  float interpolate3 = mix(dot_products[2], dot_products[6], smoothed_posi.x);
  float interpolate4 = mix(dot_products[3], dot_products[7], smoothed_posi.x);
  float interpolate5 = mix(interpolate1, interpolate3, smoothed_posi.y);
  float interpolate6 = mix(interpolate2, interpolate4, smoothed_posi.y);

  return mix(interpolate5, interpolate6, smoothed_posi.z) * 2 - 1;

  /////////////////////////////////////////////////////////////////////////////
}

