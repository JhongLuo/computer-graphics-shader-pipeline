// Generate a pseudorandom unit 3D vector
// 
// Inputs:
//   seed  3D seed
// Returns psuedorandom, unit 3D vector drawn from uniform distribution over
// the unit sphere (assuming random2 is uniform over [0,1]²).
//
// expects: random2.glsl, PI.glsl
vec3 random_direction( vec3 seed)
{
  /////////////////////////////////////////////////////////////////////////////
  vec2 random_vec = random2(seed);
  float theta = 2.0 * M_PI * random_vec.x;
  float phi = M_PI * random_vec.y;

  return normalize(vec3(sin(phi) * cos(theta), sin(phi) * sin(theta), cos(phi)));
  /////////////////////////////////////////////////////////////////////////////
}
