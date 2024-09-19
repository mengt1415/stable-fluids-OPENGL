#version 460 core
in vec2 TexCoords;
uniform sampler2D uv5;//tex1
layout(location=0) out vec3 boundarycolor;//tex0
// this shader is to maintain the boundary condition of the
// color field,after the advect wat applied, we take the advected color uv5
// as input (GL_TEXTURE1) ,and store the output back to dyetexcolorbuffer,the color attachment0 of dyefb
void main()
{
	
	boundarycolor=vec3(texture(uv5,TexCoords));

}