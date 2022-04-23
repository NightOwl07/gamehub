// rollup.config.js

import merge from 'deepmerge';
import { createBasicConfig } from '@open-wc/building-rollup';

const baseConfig = createBasicConfig();

export default merge(baseConfig, {
  input: './build/src/',
  output: [{
    file: '../../Server/resources/roleplay/client/main.js',
  }]
});