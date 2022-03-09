// Construct the model transformation matrix. The moon should orbit around the
// origin. The other object should stay still.
//
// Inputs:
//   is_moon  whether we're considering the moon
//   time  seconds on animation clock
// Returns affine model transformation as 4x4 matrix
//
// expects: identity, rotate_about_y, translate, PI
mat4 model(bool is_moon, float time)
{
  /////////////////////////////////////////////////////////////////////////////
  if (is_moon) {
    // If is_moon is true, then shrink the model by 70%, shift away
    // from the origin by 2 units and rotate around the origin at a frequency of 1
    // revolution per 4 seconds.
    return rotate_about_y(time / 2 * 3.1415926) * translate(vec3(2, 0, 0)) * uniform_scale(0.3);
  } else {
    return identity();
  }
  /////////////////////////////////////////////////////////////////////////////
}
