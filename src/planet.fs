// Generate a procedural planet and orbiting moon. Use layers of (improved)
// Perlin noise to generate planetary features such as vegetation, gaseous
// clouds, mountains, valleys, ice caps, rivers, oceans. Don't forget about the
// moon. Use `animation_seconds` in your noise input to create (periodic)
// temporal effects.
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
// expects: model, blinn_phong, bump_height, bump_position,
// improved_perlin_noise, tangent
void main()
{
  /////////////////////////////////////////////////////////////////////////////
  vec3 light;
  light = (view * rotate_about_y(-animation_seconds / 4 * 3.1415926) * vec4(1, 1, 1, 0)).xyz;


  if (is_moon) {
    vec3 ka = vec3(0.3, 0.3, 0.3);
    float random_vec1 = (improved_perlin_noise(sphere_fs_in * 5 * 0.3) + 1) / 2;
    float random_vec2 = (improved_perlin_noise(sphere_fs_in * 7 * 0.3) + 1) / 2;
    float random_vec3 = (improved_perlin_noise(sphere_fs_in * 9 * 0.3) + 1) / 2;  
    vec3 kd = vec3(random_vec1, random_vec2, random_vec3);

    vec3 n, T, B;
    n = normalize(sphere_fs_in);
    tangent(n,T,B);
    vec3 position = sphere_fs_in * (1 + (random_vec1 + random_vec2 + random_vec3) / 6);
    n = normalize(cross(bump_position(is_moon, sphere_fs_in  + B * 0.0001) - position, bump_position(is_moon, sphere_fs_in  + T * 0.0001) - position));
    
    mat4 modeling_transformation = model(is_moon, animation_seconds);
    n = normalize((view * modeling_transformation * vec4(n, 1.0) - view * modeling_transformation * vec4(0.0, 0.0, 0.0, 1.0)).xyz);


    vec3 ks = vec3(0.8, 0.8, 0.8);
    color = blinn_phong(kd, kd, ks, 100, n, normalize(-view_pos_fs_in.xyz), light);
  } else {
    float random_vec1 = (improved_perlin_noise(sphere_fs_in * 5 * 0.3) + 1) / 2;
    float random_vec2 = (improved_perlin_noise(sphere_fs_in * 7 * 0.3) + 1) / 2;
    float random_vec3 = (improved_perlin_noise(sphere_fs_in * 9 * 0.3) + 1) / 2;  
    vec3 kd = vec3((random_vec1 + 1) / 2, (random_vec2 + 1) / 2, (random_vec3 + 1) / 2);


    vec3 n, T, B;
    n = normalize(sphere_fs_in);
    tangent(n,T,B);
    vec3 position = sphere_fs_in * (1 + (random_vec1 + random_vec2 + random_vec3)/6);
    n = normalize(cross(bump_position(is_moon, sphere_fs_in  + B * 0.0001) - position, bump_position(is_moon, sphere_fs_in  + T * 0.0001) - position));
    
    mat4 modeling_transformation = model(is_moon, animation_seconds);
    n = normalize((view * modeling_transformation * vec4(n, 1.0) - view * modeling_transformation * vec4(0.0, 0.0, 0.0, 1.0)).xyz);

    vec3 ks = vec3(0.8, 0.8, 0.8);
    color = blinn_phong(kd * 0.5, kd, ks, 20, n, normalize(-view_pos_fs_in.xyz), light);
  }
  /////////////////////////////////////////////////////////////////////////////
}
