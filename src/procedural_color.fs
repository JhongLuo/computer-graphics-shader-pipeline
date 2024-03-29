// Set the pixel color to an interesting procedural color generated by mixing
// and filtering Perlin noise of different frequencies.
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
  light = (view * rotate_about_y(-animation_seconds * 3.1415926) * vec4(1, 1, 1, 0)).xyz;

  if (is_moon) {
    vec3 ka = vec3(0.3, 0.3, 0.3);
    float random_vec1 = (perlin_noise(sphere_fs_in * 5 * 0.3) + 1) / 2;
    float random_vec2 = (perlin_noise(sphere_fs_in * 7 * 0.3) + 1) / 2;
    float random_vec3 = (perlin_noise(sphere_fs_in * 9 * 0.3) + 1) / 2;  
    vec3 kd = vec3(min(0.7, random_vec1), min(0.7, random_vec2), min(0.7, random_vec3));
    vec3 ks = vec3(0.8, 0.8, 0.8);
    color = blinn_phong(kd, kd, ks, 100, normalize(normal_fs_in.xyz), normalize(-view_pos_fs_in.xyz), light);
  } else {
    float random_vec1 = (perlin_noise(sphere_fs_in * 5 * 0.3) + 1) / 2;
    float random_vec2 = (perlin_noise(sphere_fs_in * 7 * 0.3) + 1) / 2;
    float random_vec3 = (perlin_noise(sphere_fs_in * 9 * 0.3) + 1) / 2;  
    vec3 kd = vec3((random_vec1 + 1) / 2, (random_vec2 + 1) / 2, (random_vec3 + 1) / 2);
    vec3 ks = vec3(0.8, 0.8, 0.8);
    color = blinn_phong(kd * 0.5, kd, ks, 20, normalize(normal_fs_in.xyz), normalize(-view_pos_fs_in.xyz), light);
  }
  /////////////////////////////////////////////////////////////////////////////
}


