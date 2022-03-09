// Set the pixel color using Blinn-Phong shading (e.g., with constant blue and
// gray material color) with a bumpy texture.
// 
// Uniforms:
uniform mat4 view;
uniform mat4 proj;
uniform float animation_seconds;
uniform bool is_moon;
// Inputs:
//                     linearly interpolated from tessellation evaluation shader
//                     output
in vec3 sphere_fs_in;
in vec3 normal_fs_in;
in vec4 pos_fs_in; 
in vec4 view_pos_fs_in; 
// Outputs:
//               rgb color of this pixel
out vec3 color;
// expects: model, blinn_phong, bump_height, bump_position,
// improved_perlin_noise, tangent


void main()
{
  /////////////////////////////////////////////////////////////////////////////
  vec3 light;
  light = (view * rotate_about_y(-animation_seconds / 4 * 3.1415926) * vec4(1, 1, 1, 0)).xyz;

  vec3 n, T, B;
  n = normalize(sphere_fs_in);
  tangent(n,T,B);
  vec3 position = bump_position(is_moon, sphere_fs_in);
  n = normalize(cross(bump_position(is_moon, sphere_fs_in  + B * 0.0001) - position, bump_position(is_moon, sphere_fs_in  + T * 0.0001) - position));
  
  mat4 modeling_transformation = model(is_moon, animation_seconds);
  n = normalize((view * modeling_transformation * vec4(n, 1.0) - view * modeling_transformation * vec4(0.0, 0.0, 0.0, 1.0)).xyz);

  if (is_moon) {
    vec3 ka, kd, ks;
    ka = vec3(0.46, 0.4, 0.46);
    kd = vec3(0.46, 0.4, 0.46);
    ks = vec3(0.46, 0.4, 0.46);
    color = blinn_phong(ka, kd, ks, 800, n, normalize(-view_pos_fs_in.xyz), light);
  } else {
    vec3 ka, kd, ks;
    ka = vec3(0.5, 0.5, 0.5);
    kd = vec3(0.1, 0.1, 1);
    ks = vec3(0.5, 0.5, 0.5);
    color = blinn_phong(ka, kd, ks, 800, n, normalize(-view_pos_fs_in.xyz), light);
  }
  /////////////////////////////////////////////////////////////////////////////
}
