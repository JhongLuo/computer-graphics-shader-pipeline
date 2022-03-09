// Add (hard code) an orbiting (point or directional) light to the scene. Light
// the scene using the Blinn-Phong Lighting Model.
//
// Uniforms:
uniform mat4 view;
uniform mat4 proj;
uniform float animation_seconds;
uniform bool is_moon;
// Inputs:
in vec3 sphere_fs_in;
in vec3 normal_fs_in;
in vec4 pos_fs_in; 
in vec4 view_pos_fs_in; 
// Outputs:
out vec3 color;
// expects: PI, blinn_phong
mat4 rotate_about_y(float theta)
{
  return mat4(
  cos(theta), 0, -sin(theta), 0,
  0,          1, 0,           0,
  sin(theta), 0, cos(theta),  0,
  0,          0, 0,           1);
}

void main()
{
  /////////////////////////////////////////////////////////////////////////////
  vec3 light;
  light = (view * rotate_about_y(-animation_seconds / 3 * 3.1415926) * vec4(1, 1, 1, 0)).xyz;

  if (is_moon) {
    vec3 ka, kd, ks;
    ka = vec3(0.46, 0.4, 0.46);
    kd = vec3(0.46, 0.4, 0.46);
    ks = vec3(0.46, 0.4, 0.46);
    color = blinn_phong(ka, kd, ks, 800, normalize(normal_fs_in.xyz), normalize(-view_pos_fs_in.xyz), light);
  } else {
    vec3 ka, kd, ks;
    ka = vec3(0.5, 0.5, 0.5);
    kd = vec3(0.1, 0.1, 1);
    ks = vec3(0.5, 0.5, 0.5);
    color = blinn_phong(ka, kd, ks, 800, normalize(normal_fs_in.xyz), normalize(-view_pos_fs_in.xyz), light);
  }
  /////////////////////////////////////////////////////////////////////////////
}
